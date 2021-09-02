using System;
using System.Collections.Generic;
using System.Text;

namespace EFPractice.Database {
  class ImageScoreListDetail {
    public int Id { get; set; }
    public int ScorerId { get; set; }
    public int ScorerValue { get; set; }
    public string Timing { get; set; }
    public string ScTimestamp { get; set; }
    public int? ScoreFormat { get; set; }


    ImageScoreListDetail() { }
  }
}
