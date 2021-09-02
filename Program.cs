using EFPractice.Database;
using EFPractice.ProcessControl;
using System;
using System.Linq;

namespace EFPractice {
  class Program {
    static void Main(string[] args) {
      Console.WriteLine("Entity Framework Practice by Razif!");
      /*ProcessManager prc = new ProcessManager();
      prc.KillProcessByPortNumber(5432);*/

      FSSContext context = new FSSContext();
      var data = context.ImageScoreListDetail
                    .First();
      Console.WriteLine(data.Timing);
      // context.Test();
    }
  }
}
