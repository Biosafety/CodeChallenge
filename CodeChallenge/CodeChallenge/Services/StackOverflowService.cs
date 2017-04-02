using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using CodeChallenge.Models;
using Newtonsoft.Json;

namespace CodeChallenge.Services
{
    public class StackOverflowService
    {
        public async Task<StackOverflow> GetQuestionsAsync(int page)
        {
            StackOverflow questions, allAs = new StackOverflow();
            allAs = await GetStackAnswers(page);
            //List<int> q = allAs.items.Where(a => a.is_answered).ToList().Select(a => a.question_id);
            List<int> q = new List<int>();
            foreach(StackItem item in allAs.items)
            {
                if(item.is_answered == true)
                {
                    q.Add(item.question_id);
                }
            }

            questions = await GetStackQuestions(q);

            return questions;
        }

        private async Task<StackOverflow> GetStackAnswers(int page)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.stackexchange.com/");

            HttpRequestMessage message = new HttpRequestMessage(new HttpMethod("GET"), "/2.2/answers?page=" + page + "&pagesize=100&order=desc&sort=activity&site=stackoverflow");
            HttpResponseMessage response = await client.SendAsync(message);

            string json = await response.Content.ReadAsStringAsync();

            StackOverflow answers = new StackOverflow();
            answers = JsonConvert.DeserializeObject<StackOverflow>(json);

            return answers;
        }

        private async Task<StackOverflow> GetStackQuestions(List<int> ids)
        {
            var client = new HttpClient();
            client.BaseAddress = new Uri("https://api.stackexchange.com/");

            string urlIds = "";
            foreach(int id in ids)
            {
                if (id == ids.LastOrDefault())
                {
                    urlIds += id;
                } else
                {
                    urlIds += id + ";";
                }
            }

            HttpRequestMessage message = new HttpRequestMessage(new HttpMethod("GET"), "/2.2/questions?page=" + urlIds + "&order=desc&sort=activity&site=stackoverflow");
            HttpResponseMessage response = await client.SendAsync(message);

            string json = await response.Content.ReadAsStringAsync();

            StackOverflow questions = new StackOverflow();
            questions = JsonConvert.DeserializeObject<StackOverflow>(json);

            return questions;
        }
    }
}
