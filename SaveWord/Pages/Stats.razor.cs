using SaveWord.Data;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveWord.Pages
{
    public class StatsBase : ComponentBase
    {
        [Inject]
        WordService wordService { get; set; }

        List<Word> words;

        [Parameter]
        public int WordCounter { get { return wordService.GetWords().Count; } set { } }
        [Parameter]
        public int TestedWords { get { return wordService.GetTestedCount(); } set { } }
        [Parameter]
        public int CorrectCount { get { return wordService.GetCorrectCount(); } set { } }
        [Parameter]
        public int WrongCount { get { return TestedWords - CorrectCount; } set { } }

        public List<Word> Words { get { return words; } }

        protected override void OnInitialized()
        {
            GetWords();
        }

        private void GetWords()
        {
            words = wordService.GetWords();
        }
    }
}
