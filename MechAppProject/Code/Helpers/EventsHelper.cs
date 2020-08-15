using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MechAppProject.Code.Helpers
{
    public class EventsHelper
    {
        public static IEnumerable<SelectListItem> GetHoursToSelect(int startHour, int endHour, int stepInMinutes)
        {
            var hoursToSelect = new List<SelectListItem>();

            for (int i = startHour; i < endHour; i++)
            {
                var condition = 0;

                while (condition < 60)
                {
                    var minutes = condition.ToString();

                    if (condition < 10)
                    {
                        minutes = "0" + minutes;
                    }

                    var hourFormat = i + ":" + minutes;

                    hoursToSelect.Add(new SelectListItem() { Text = hourFormat, Value = hourFormat });
                    condition += stepInMinutes;
                }
            }

            return hoursToSelect;
        }
    }
}