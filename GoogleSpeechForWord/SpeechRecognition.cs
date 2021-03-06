﻿using System;
using System.Reflection;
using System.Resources;
using System.Threading;
using System.Threading.Tasks;
using Google.Cloud.Speech.V1;
using log4net;

namespace GoogleSpeechForWord
{
    class SpeechRecognition
    {
        private static readonly ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private HandlerAddIn handler;

        ResourceManager resourceManager;

        public SpeechRecognition(HandlerAddIn handler, ResourceManager resourceManager)
        {
            this.handler = handler;
            this.resourceManager = resourceManager;
        }

        public void StartListening(int seconds)
        {
            StreamingMicRecognizeAsync(seconds);
        }

        public async Task<object> StreamingMicRecognizeAsync(int seconds)
        {
            if (NAudio.Wave.WaveIn.DeviceCount < 1)
            {
                log.Debug("No microphone!");
                return -1;
            }
            
            var speech = SpeechClient.Create();
            var streamingCall = speech.StreamingRecognize();
            // Write the initial request with the config.
            await streamingCall.WriteAsync(
                new StreamingRecognizeRequest()
                {
                    StreamingConfig = new StreamingRecognitionConfig()
                    {
                        Config = new RecognitionConfig()
                        {
                            Encoding =
                            RecognitionConfig.Types.AudioEncoding.Linear16,
                            SampleRateHertz = 16000,
                            LanguageCode = "pl",
                        },
                        InterimResults = true,
                    }
                });
            // Print responses as they arrive.
            Task printResponses = Task.Run(async () =>
            {
                int mode = -1;
                while (await streamingCall.ResponseStream.MoveNext(
                    default(CancellationToken)))
                {
                    foreach (var result in streamingCall.ResponseStream
                        .Current.Results)
                    {
                        foreach (var alternative in result.Alternatives)
                        {
                            if(result.IsFinal)
                            {
                                if (mode == -1)
                                {
                                    var firstWord = alternative.Transcript.IndexOf(" ") > -1
                                          ? alternative.Transcript.Substring(0, alternative.Transcript.IndexOf(" "))
                                          : alternative.Transcript;
                                    if (firstWord.ToLower() == resourceManager.GetString("text")) mode = 1;
                                    else if (firstWord.ToLower() == resourceManager.GetString("sign")) mode = 2;
                                    else if (firstWord.ToLower() == resourceManager.GetString("command")) mode = 3;
                                }
                                else
                                {
                                    log.Debug("Working in mode " + mode);
                                    switch (mode)
                                    {
                                        case 1:
                                            handler.InsertText(alternative.Transcript);
                                            break;
                                        case 2:
                                            handler.InsertSign(alternative.Transcript);
                                            break;
                                        case 3:
                                            handler.IssueCommand(alternative.Transcript);
                                            break;
                                    }
                                }
                                
                            }
                            
                        }
                    }
                }
            });
            // Read from the microphone and stream to API.
            object writeLock = new object();
            bool writeMore = true;
            var waveIn = new NAudio.Wave.WaveInEvent();
            waveIn.DeviceNumber = 0;
            waveIn.WaveFormat = new NAudio.Wave.WaveFormat(16000, 1);
            waveIn.DataAvailable +=
                (object sender, NAudio.Wave.WaveInEventArgs args) =>
                {
                    lock (writeLock)
                    {
                        if (!writeMore) return;
                        streamingCall.WriteAsync(
                            new StreamingRecognizeRequest()
                            {
                                AudioContent = Google.Protobuf.ByteString
                                    .CopyFrom(args.Buffer, 0, args.BytesRecorded)
                            }).Wait();
                    }
                };
            waveIn.StartRecording();
            log.Debug("Speak now.");
            await Task.Delay(TimeSpan.FromSeconds(seconds));
            // Stop recording and shut down.
            waveIn.StopRecording();
            lock (writeLock) writeMore = false;
            await streamingCall.WriteCompleteAsync();
            await printResponses;
            return 0;
        }
    }
}
