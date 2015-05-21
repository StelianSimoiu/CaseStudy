using Octokit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CaseStudy
{
    static class Routines
    {
        public static void ListStarred(int starred, string name)
        {
            Console.WriteLine("{0} starred repositories for user: {1}", starred, name);
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
