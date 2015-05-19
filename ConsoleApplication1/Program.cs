using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ConsoleApplication1
{
    class Program
    {
        private static Lazy<GitHubClient> github = new Lazy<GitHubClient>(() => new GitHubClient(new ProductHeaderValue("CaseStudy")));
        public static GitHubClient GitHub { get { return github.Value; } }

        static void Main(string[] args)
        {
            if((0 == args.Count()) || (args.Count() > 2))
            {
                Console.WriteLine("One or Two parameters only.");
                Environment.Exit(-1);
            }

            var repoList = GetRepository(args[0]);
            ListRepository(repoList);
            GetStarred(repoList);
  
        }

        static void GetStarred(Task<IReadOnlyList<Repository>> repository)
        {
            int starred = repository.Result.Count(n => n.StargazersCount > 0);
            Console.WriteLine("Starred: {0}",starred);
        }
       static Task<IReadOnlyList<Repository>> GetRepository(string userName)
        {
            var repository = GitHub.Repository.GetAllForUser(userName);
            repository.Wait();
            return repository;
        }
        static void ListRepository(Task<IReadOnlyList<Repository>> repository)
       {
           Console.WriteLine("Repository list: ");
           foreach (Repository repo in repository.Result)
           {
               Console.WriteLine(repo.Name);
           }
           Console.WriteLine("");
       }
    }
}
