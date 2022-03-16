using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveWord.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;

namespace SaveWord.Pages
{
    public class AddWordBase : ComponentBase
    {
        [Inject]
        WordService wordService { get; set; }

        public Word Word { get; set; } = new Word();

        private List<Word> words;

        public bool ShowAddedNotification { get; set; }


        protected override void OnInitialized()
        {
            GetWords();
        }

        private void AddWord()
        {
            Word word = new Word()
            {
                WordText = "test",
                MeaningText = "trying to smth. if it works or not.",
                SentenceText = "let's test it."
            };

            wordService.AddWord(word);
            GetWords();
        }

        private void GetWords()
        {
            words = wordService.GetWords();
        }

        protected async Task SubmitWord(EditContext editContext)
        {

            wordService.AddWord((Word)editContext.Model);
            await ShowNotification();
            //AddWord();
            GetWords();
        }

        protected async Task ShowNotification()
        {
            ShowAddedNotification = true;
            //wordService.EditWord(words[Questions[ActiveQuestion].Id - 1]);
            await Task.Delay(3000);
            StateHasChanged();
            ShowAddedNotification = false;
        }
    }
}
