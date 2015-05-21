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
        static GitHub Git = new GitHub();
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
            var repoList = Git.GetRepository(args[0]);
            Routines.ListRepository(repoList);
        }
        static void CaseTwo(string[] args)
        {
            var repoListOne = Git.GetRepository(args[0]);
            var repoListTwo = Git.GetRepository(args[1]);
            int starredOne = Git.GetStarred(repoListOne);
            int starredTwo = Git.GetStarred(repoListTwo);

            Routines.ListStarred(starredOne, args[0]);
            Routines.ListStarred(starredTwo, args[1]);

            Console.WriteLine((starredOne == starredTwo ? "Equal." : (starredOne > starredTwo ? "First repository has more stars." : "Second repository has more stars.")));
        }
    }
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
            var repository = Client.Repository.GetAllForUser(userName);
            repository.Wait();
            return repository;
        }
    }
    static class Routines
    {
        public static void ListStarred(int starred, string name)
        {
            Console.WriteLine("{0} starred repositories for user: {1}",starred, name);
        }
        public static void ListRepository(Task<IReadOnlyList<Repository>> repository)
        {
            Console.WriteLine("");
            Console.WriteLine("Repository list: ");
            foreach (Repository repo in repository.Result)
            {
                Console.WriteLine(repo.Name);
            }
            Console.WriteLine("");
        }
    }

}
