using SaveWord.Data;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SaveWord.Pages
{


    public class Question 
    {
        public Word word;
        public int correctIndex;
        public int Id { get; set; }
        public string QuestionText { get; set; }
        
        public string correctAnswer;
        public List<string> Answers { get; set; }
    }

    public class TestYourselfBase: ComponentBase
    {
        [Inject]
        WordService wordService { get; set; }

        List<Word> words;

        [Parameter]
        public int QuestionCount { get; set; }

        protected int ActiveQuestion;
        protected bool IsDisabled;
        protected bool TestActive;
        protected bool EnoughAmount { get { return words.Count > 49; } }

        public List<Word> Words { get { return words; } }
        public List<Question> Questions { get; set; }

        protected int correctCounter;
        protected int falseCounter;

        protected string[] colors = new string[4];
        protected bool IsDisabledAnswer;

        protected override void OnInitialized()
        {
            GetWords();
            IsDisabled = !EnoughAmount;
        }

        private void GetWords()
        {
            words = wordService.GetWords();
        }

        protected void StartTest()
        {
            GetWords();
            ActiveQuestion = 0;
            correctCounter = 0;
            falseCounter = 0;
			StateHasChanged();
            IsDisabled = true;
			CreateTest(QuestionCount);
            TestActive = true;

            colors[0] = "#6c757d";
            colors[1] = "#6c757d";
            colors[2] = "#6c757d";
            colors[3] = "#6c757d";
        }

        protected async Task CheckAnswer(int index) 
        {
            if (TestActive) 
            {
                //GetWords();

                colors[0] = "#000000";
                colors[1] = "#000000";
                colors[2] = "#000000";
                colors[3] = "#000000";

                if (Questions[ActiveQuestion].correctAnswer == Questions[ActiveQuestion].Answers[index])
                {
                    correctCounter++;
                    Questions[ActiveQuestion].word.IsCorrect = true;
                    colors[Questions[ActiveQuestion].correctIndex] = "#00FF00";
                    //words[Questions[ActiveQuestion].Id - 1].IsCorrect = true;
                }
                else
                {
                    falseCounter++;
                    Questions[ActiveQuestion].word.IsCorrect = false;
                    colors[index] = "#FF0000";
                    colors[Questions[ActiveQuestion].correctIndex] = "#00FF00";

                    //words[Questions[ActiveQuestion].Id - 1].IsCorrect = false;
                }

                // set as tested
                // we have to decrease the Id because the rows start from index 1 in sqlite
                Questions[ActiveQuestion].word.IsTested = true;
                //words[Questions[ActiveQuestion].Id - 1].IsTested = true;
                wordService.EditWord(Questions[ActiveQuestion].word);
 
                IsDisabledAnswer = true;
                //wordService.EditWord(words[Questions[ActiveQuestion].Id - 1]);
                await Task.Delay(3000);
                IsDisabledAnswer = false;

                if (ActiveQuestion + 1 == QuestionCount)
                    TestOver();
                else
                    ActiveQuestion++;

                colors[0] = "#6c757d";
                colors[1] = "#6c757d";
                colors[2] = "#6c757d";
                colors[3] = "#6c757d";
            }
        }

        private void TestOver() 
        {
            ActiveQuestion = 0;
            TestActive = false;
            IsDisabled = false;
        }

        private void CreateTest(int questionCount) 
        {
            if(questionCount > words.Count) 
            {
                QuestionCount = words.Count;
            }

            GetWords();

            Questions = new List<Question>();

            Dictionary<int, string> chosens = new Dictionary<int, string>();

            List<Word> chosenWords = wordService.QueryWordRandUnique(questionCount);


            for (int i = 0; i < QuestionCount; i++)
            {
				int a2, a3, a4;

				a2 = new Random().Next(words.Count);
				a3 = new Random().Next(words.Count);
				a4 = new Random().Next(words.Count);

				if (words[a2].MeaningText == chosenWords[i].MeaningText) a2++;
				if (a2 >= words.Count) a2 += -2;
				if (words[a3].MeaningText == chosenWords[i].MeaningText) a3++;
				if (a3 >= words.Count) a3 += -3;
				if (words[a4].MeaningText == chosenWords[i].MeaningText) a4++;
				if (a4 >= words.Count) a4 += -4;

				Question question = new Question();
                question.word = chosenWords[i];
                question.Id = chosenWords[i].rowid;
                question.QuestionText = chosenWords[i].WordText;
				question.Answers = new List<string> { "", "", "", "" };
				question.correctAnswer = chosenWords[i].MeaningText;
				
				List<int> indexList = new List<int> { 0, 1, 2, 3 };

				for (int j = 0; j < 4; j++)
				{
					int t = indexList[new Random().Next(4 - j)];
					indexList.Remove(t);
					if (t == 0)
					{
                        question.correctIndex = j;
						question.Answers[j] = chosenWords[i].MeaningText;
					}
					else if (t == 1) question.Answers[j] = words[a2].MeaningText;
					else if (t == 2) question.Answers[j] = words[a3].MeaningText;
					else if (t == 3) question.Answers[j] = words[a4].MeaningText;
				}

				Questions.Add(question);
            }
        }
    }
}