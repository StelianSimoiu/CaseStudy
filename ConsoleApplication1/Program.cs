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
}
