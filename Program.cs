using EFPractice.Business;
using EFPractice.Database;
using EFPractice.ProcessControl;
using System;
using System.Collections.Generic;
using System.Linq;

namespace EFPractice {
  class Program {
    static void Main(string[] args) {
      Console.WriteLine("Entity Framework Practice by Razif!");
      /*ProcessManager prc = new ProcessManager();
      prc.KillProcessByPortNumber(5432);*/

      FSSContext context = new FSSContext();
      DisagreementFilter df = new DisagreementFilter(context);
      List<int> disagreemntId = df.GetDisagreementId();
      Console.WriteLine($"There are { disagreemntId.Count() } disagreements");

      /*var data = context.ImageScoreListDetail
                    .Where(p => p.ScorerId == 115);*/

      /*Console.WriteLine(data.Count());
      Console.ReadKey();*/
      // context.Test();
    }
  }
}
