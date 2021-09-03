using EFPractice.Database;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EFPractice.Business {
  class DisagreementFilter {
    DbSet<ImageScoreListDetail> _data;
    internal DisagreementFilter(FSSContext context) {
      _data = context.ImageScoreListDetail;
    }

    internal List<int> GetDisagreementId() {
      var result = from dis in
                     (from i in _data
                      group i by new { i.Id, i.ScorerValue } into groupRes
                      where groupRes.Count() < 3
                      select new {
                        id = groupRes.Key.Id,
                        num = groupRes.Count(),
                      })
                   group dis by dis.id into groupDis
                   orderby groupDis.Key
                   select groupDis.Key;
      return result.ToList();
    }
  }
}