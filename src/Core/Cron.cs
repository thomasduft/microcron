using System;
using System.Collections.Generic;
using System.Linq;

namespace tomware.Microcron.Core
{
  /// <summary>
  /// Simple cron expression parser.
  /// </summary>
  /// <code>
  /// Supported format:
  ///  * * * * * command to execute
  ///  │ │ │ │ └── day of week (0 - 6 or SUN-SAT)
  ///  │ │ │ └──── month (1 - 12 or JAN-DEC)
  ///  │ │ └────── day of month (1 - 31)
  ///  │ └──────── hour (0 - 23)
  ///  └────────── min (0 - 59)
  /// </code>
  public class Cron
  {
    private List<CronFieldBase> _fields;

    public string Expression { get; private set; }

    /// <summary>
    /// Creates a empty Cron object with the default expression <* * * * *>.
    /// </summary>
    public Cron()
    {
      Expression = Expressions.DEFAULT_EXPRESSION;

      this.init();
    }

    /// <summary>
    /// Creates a Cron object with the desired expression.
    /// </summary>
    /// <param name="expression"></param>
    public Cron(string expression)
    {
      Expression = expression;

      this.init();
    }

    /// <summary>
    /// Returns the datetime of the next occurence based on a given reference datetime.
    /// </summary>
    /// <param name="reference"></param>
    /// <returns></returns>
    public DateTime GetNextOccurrence(DateTime reference)
    {
      var now = reference;
      if (now.Second > 0) now = now.AddSeconds(-now.Second);

      while (now < DateTime.MaxValue)
      {
        now = now.AddMinutes(1);

        if (Match(now)) return now;
      }

      throw new Exception("DateTime.MaxValue reached!");
    }

    private void init()
    {
      _fields = new List<CronFieldBase>(5);
      ParseExpression();
    }

    private void ParseExpression()
    {
      var elements = Expression.Split(' ');
      if (elements.Length != 5)
        throw new Exception("Expression is not a valid 5 token based cron expression!");

      _fields.Add(new MinutesCronField
      {
        CronFieldType = CronFieldType.Minutes,
        ExpressionValue = elements[0]
      });
      _fields.Add(new HoursCronField
      {
        CronFieldType = CronFieldType.Hours,
        ExpressionValue = elements[1]
      });
      _fields.Add(new DayOfMonthCronField
      {
        CronFieldType = CronFieldType.DayOfMonth,
        ExpressionValue = elements[2]
      });
      _fields.Add(new MonthCronField
      {
        CronFieldType = CronFieldType.Month,
        ExpressionValue = elements[3]
      });
      _fields.Add(new DayOfWeekCronField
      {
        CronFieldType = CronFieldType.DayOfWeek,
        ExpressionValue = elements[4]
      });

      foreach (var item in _fields)
      {
        item.Parse();
      }
    }

    private CronFieldBase GetField(CronFieldType type)
    {
      return _fields.First(f => f.CronFieldType == type);
    }

    private bool Match(DateTime now)
    {
      bool match = GetField(CronFieldType.Minutes).Contains(now.Minute);
      match &= GetField(CronFieldType.Hours).Contains(now.Hour);
      match &= GetField(CronFieldType.DayOfMonth).Contains(now.Day);
      match &= GetField(CronFieldType.Month).Contains(now.Month);
      match &= GetField(CronFieldType.DayOfWeek).Contains((int)now.DayOfWeek);
      return match;
    }
  }

  public class Expressions
  {
    public const string DEFAULT_EXPRESSION_FIELD = "*";
    public const string DEFAULT_EXPRESSION = "* * * * *";
  }

  internal enum CronFieldType
  {
    Minutes,
    Hours,
    DayOfMonth,
    Month,
    DayOfWeek
  }

  internal abstract class CronFieldBase
  {
    internal CronFieldType CronFieldType { get; set; }
    internal string ExpressionValue { get; set; }
    internal ICollection<int> FieldValues { get; set; }

    internal abstract void Parse();
    internal bool Contains(int timePart)
    {
      return this.FieldValues.Contains(timePart);
    }
  }

  internal class MinutesCronField : CronFieldBase
  {
    internal override void Parse()
    {
      // min (0 - 59)
      var minutes = this.ExpressionValue.Split(',');
      List<int> givenMinutes = new List<int>();
      foreach (var minute in minutes)
      {
        if (minute == Expressions.DEFAULT_EXPRESSION_FIELD)
        {
          for (int i = 0; i <= 59; i++)
          {
            givenMinutes.Add(i);
          }
        }
        else
        {
          int m;
          if (!int.TryParse(minute, out m)) continue;

          givenMinutes.Add(m);
        }
      }

      this.FieldValues = givenMinutes.ToArray();
    }
  }

  internal class HoursCronField : CronFieldBase
  {
    internal override void Parse()
    {
      // hour	(0 - 23)
      var hours = this.ExpressionValue.Split(',');
      List<int> givenHours = new List<int>();
      foreach (var hour in hours)
      {
        if (hour == Expressions.DEFAULT_EXPRESSION_FIELD)
        {
          for (int i = 0; i <= 23; i++)
          {
            givenHours.Add(i);
          }
        }
        else
        {
          int h;
          if (!int.TryParse(hour, out h)) continue;

          givenHours.Add(h);
        }
      }

      this.FieldValues = givenHours.ToArray();
    }
  }

  internal class DayOfMonthCronField : CronFieldBase
  {
    internal override void Parse()
    {
      // day of month	(1 - 31)
      var days = this.ExpressionValue.Split(',');
      List<int> givenDays = new List<int>();
      foreach (var day in days)
      {
        if (day == Expressions.DEFAULT_EXPRESSION_FIELD)
        {
          for (int i = 1; i <= 31; i++)
          {
            givenDays.Add(i);
          }
        }
        else
        {
          int d;
          if (!int.TryParse(day, out d)) continue;

          givenDays.Add(d);
        }
      }

      this.FieldValues = givenDays.ToArray();
    }
  }

  internal class MonthCronField : CronFieldBase
  {
    internal static string[] MONTHS = {
      "JAN",
      "FEB",
      "MAR",
      "APR",
      "MAI",
      "JUN",
      "JUL",
      "AUG",
      "OKT",
      "NOV",
      "DEZ"
      };

    internal override void Parse()
    {
      // month (1 - 12 or JAN-DEC)
      var months = this.ExpressionValue.Split(',');
      List<int> givenMonths = new List<int>();
      foreach (var month in months)
      {
        if (month == Expressions.DEFAULT_EXPRESSION_FIELD)
        {
          for (int i = 1; i <= 12; i++)
          {
            givenMonths.Add(i);
          }
        }
        else
        {
          int m;
          if (!int.TryParse(month, out m))
          {
            m = GetMonth(month);
          }
          givenMonths.Add(m);
        }
      }

      this.FieldValues = givenMonths.ToArray();
    }

    private int GetMonth(string month)
    {
      var index = 1;
      foreach (var item in MONTHS)
      {
        if (item.Equals(month, StringComparison.OrdinalIgnoreCase))
        {
          return index;
        }

        index++;
      }

      var monthText = String.Join(", ", MONTHS);
      var message = string.Format(
        "Month '{0}' is not supported! Supported names are: '{1}'.",
        month,
        monthText
      );
      throw new NotSupportedException(message);
    }
  }

  internal class DayOfWeekCronField : CronFieldBase
  {
    internal static string[] WEEKDAYS = {
      "SUN",
      "MON",
      "TUE",
      "WED",
      "THU",
      "FRI",
      "SAT"
    };

    internal override void Parse()
    {
      // day of week (0 - 6 or SUN-SAT)
      var weekdays = this.ExpressionValue.Split(',');
      List<int> givenWeekdays = new List<int>();
      foreach (var weekday in weekdays)
      {
        if (weekday == Expressions.DEFAULT_EXPRESSION_FIELD)
        {
          for (int i = 0; i <= 6; i++)
          {
            givenWeekdays.Add(i);
          }
        }
        else
        {
          int d;
          if (!int.TryParse(weekday, out d))
          {
            d = GetDayOfWeek(weekday);
          }
          givenWeekdays.Add(d);
        }
      }

      this.FieldValues = givenWeekdays.ToArray();
    }

    private int GetDayOfWeek(string weekday)
    {
      var index = 0;
      foreach (var item in WEEKDAYS)
      {
        if (item.Equals(weekday, StringComparison.OrdinalIgnoreCase))
        {
          return index;
        }

        index++;
      }

      var text = String.Join(", ", WEEKDAYS);
      var message = string.Format(
        "Weekday '{0}' is not supported! Supported names are: '{1}'.",
        weekday,
        text
      );
      throw new NotSupportedException(message);
    }
  }
}