using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy
{
    public class GitHub
    {
        private static Lazy<GitHubClient> client = new Lazy<GitHubClient>(() => new GitHubClient(new ProductHeaderValue("CaseStudy")));
        public static GitHubClient Client { get { return client.Value; } }
        public int GetStarred(Task<IReadOnlyList<Repository>> repository)
        {
            return repository.Result.Count(n => n.StargazersCount > 0);
        }
        public Task<IReadOnlyList<Repository>> GetRepository(string userName)
        {
            Task<IReadOnlyList<Repository>> repository = null;
            try
            {
                repository = Client.Repository.GetAllForUser(userName);
                repository.Wait();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                Console.WriteLine(e.InnerException);
                Environment.Exit(-2);
            }
            return repository;
        }
    }
}
