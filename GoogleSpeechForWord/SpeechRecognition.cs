using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
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

        public SpeechRecognition(HandlerAddIn handler)
        {
            this.handler = handler;
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
                            LanguageCode = "en",
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
                                    if (firstWord.ToLower() == "text") mode = 1;
                                    else if (firstWord.ToLower() == "sign") mode = 2;
                                    else if (firstWord.ToLower() == "command") mode = 3;
                                }
                                else
                                {
                                    if (mode == 1)
                                    {
                                        log.Debug("Working in mode 1");
                                        handler.InsertText(alternative.Transcript);
                                    }
                                    else if (mode == 2)
                                    {
                                        log.Debug("Working in mode 2");
                                        handler.InsertSign(alternative.Transcript);
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
