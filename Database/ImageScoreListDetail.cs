using System;
using System.Collections.Generic;
using System.Text;
using System.ComponentModel.DataAnnotations.Schema;
using NpgsqlTypes;
using System.Linq;
using System.Reflection;

namespace EFPractice.Database {
  [Table("imagescorelistdetail")]
  class ImageScoreListDetail {
    [Column("id")]
    public int Id { get; set; }

    [Column("scorer_id")]
    public int ScorerId { get; set; }

    [Column("scorer_value")]
    public int ScorerValue { get; set; }

    [Column("timing")]
    public TimeSpan Timing { get; set; }

    [Column("sc_timestamp")]
    public DateTime ScTimestamp { get; set; }

    [Column("scoreformat")]
    public int? ScoreFormat { get; set; }

    ImageScoreListDetail() { }
  }
}
