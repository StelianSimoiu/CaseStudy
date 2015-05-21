using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace CaseStudy
{
    class Program
    {
        private static Lazy<GitHubClient> github = new Lazy<GitHubClient>(() => new GitHubClient(new ProductHeaderValue("CaseStudy")));
        public static GitHubClient GitHub { get { return github.Value; } }

        static void Main(string[] args)
        {
            switch (args.Count())
            {
                case 0: { Console.WriteLine("No parameters. Exit."); break; }
                case 1: { CaseOne(args); break; }
                case 2: { CaseTwo(args); ; break; }
                default: { Console.WriteLine("One or Two parameters only."); ; break; }
            }
        }
        static void CaseOne(string[] args)
        {
            var repoList = GetRepository(args[0]);
            ListRepository(repoList);
            GetStarred(repoList);
        }
        static void CaseTwo(string[] args)
        {
            var repoListOne = GetRepository(args[0]);
            var repoListTwo = GetRepository(args[1]);
            int starredOne = GetStarred(repoListOne);
            int starredTwo = GetStarred(repoListTwo);

            Console.WriteLine("{0}: {1} starred repositories.", args[0], starredOne);
            Console.WriteLine("{0}: {1} starred repositories.", args[1], starredTwo);
            Console.WriteLine((starredOne == starredTwo ? "Equal." : (starredOne > starredTwo ? "First repository has more stars." : "Second repository has more stars.")));
        }
        static int GetStarred(Task<IReadOnlyList<Repository>> repository)
        {
            return repository.Result.Count(n => n.StargazersCount > 0);
            
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
