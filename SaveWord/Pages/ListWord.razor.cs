using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SaveWord.Data;
using Microsoft.AspNetCore.Components;

namespace SaveWord.Pages
{
    public class ListWordBase: ComponentBase
    {
        [Inject]
        WordService wordService { get; set; }

        List<Word> words;

        public string SearchText = "";

        public List<Word> Words { get { return words; } }

        protected List<Word> FilteredWords => words.Where(
            word => word.WordText.ToLower().Contains(SearchText.ToLower())).ToList();

        protected override void OnInitialized()
        {
            GetWords();
        }

        private void GetWords() 
        {
            words = wordService.GetWords();
        }

        protected void Enable(bool isEnable, Word word)
        {
            word.EditState = isEnable;
        }

        protected void EditWord(Word word) 
        {
            Enable(false, word);
            wordService.EditWord(word);
            GetWords();
        }


        protected void RemoveWord(Word word) 
        {
            wordService.DeleteWord(word);
            GetWords();
        }
    }
}
