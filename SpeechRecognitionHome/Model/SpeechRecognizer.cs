using System;
using Windows.Media.SpeechRecognition;

namespace SpeechRPi.Model
{
    public class SpeechRecognizer
    {
        private Windows.Media.SpeechRecognition.SpeechRecognizer speechRecognizer;

        public SpeechRecognizer()
        {
            this.InitializeSpeechRecognizer();
        }
      
        public async void StartRecognizingSpeech()
        {
            SpeechRecognitionResult speechRecognitionResult = await speechRecognizer.RecognizeAsync();

            if (speechRecognitionResult.Status == SpeechRecognitionResultStatus.Success)
            {
                this.OnSpeechRecognized(speechRecognitionResult.Text);
            }
            else
            {
                this.OnSpeechRecognitionError();
            }
        }

        public event EventHandler SpeechRecognitionError;
        protected virtual void OnSpeechRecognitionError()
        {
            this.SpeechRecognitionError?.Invoke(this, EventArgs.Empty);
        }

        public event EventHandler<SpeechRecognizedEventArgs> SpeechRecognized;
        protected virtual void OnSpeechRecognized(string recognizedText)
        {
            this.SpeechRecognized?.Invoke(this, new SpeechRecognizedEventArgs(recognizedText));
        }

        private async void InitializeSpeechRecognizer()
        {
            this.speechRecognizer = new Windows.Media.SpeechRecognition.SpeechRecognizer();
            await this.speechRecognizer.CompileConstraintsAsync();
        }

    }
}
