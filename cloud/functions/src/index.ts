import * as functions from "firebase-functions";
import * as admin from "firebase-admin";
import axios from 'axios';
import cheerio from 'cheerio';

const BASE_URL = "https://zwiftinsider.com/schedule/";

admin.initializeApp();

interface World {
  name: string;
  link: string;
}

interface CalendarDay {
  day: number;
  worlds: World[];
}

interface CalendarMonth {
  year: number;
  month: number;
  days: CalendarDay[];
}

function getMonthIndex(month: string) : number {
  const months = [ "january", "february", "march", "april", "may", "june", "july", "august", "september", "october", "november", "december" ];
  return months.indexOf(month.toLowerCase()) + 1;
}

function getMonthAbbr(monthIdx: number): string {
  const monthAbbrs = [ "jan", "feb", "mar", "apr", "may", "jun", "jul", "aug", "sep", "oct", "nov", "dec" ];
  return monthAbbrs[monthIdx - 1];
}

const scrapeCalendar = async (year: number, month: number) : Promise<CalendarMonth> => {
  const url = BASE_URL + "?month=" + getMonthAbbr(month) + "&yr=" + year;
  functions.logger.log('Requesting url: ', url);

  const axiosInstance = axios.create();
  const response = await axiosInstance.get(url);

  const html = response.data;
  const $ = cheerio.load(html);

  const calendarTable = $('.calendar-table');

  // Get the month that the calendar is for (in format of 'MMMM YYYY')
  const calendarHeader = calendarTable.find('tr.calendar-heading td.calendar-month').text();
  const rxYear = parseInt(calendarHeader.split(' ')[1]);
  const rxMonth = getMonthIndex(calendarHeader.split(' ')[0]);
  const calendarMonth: CalendarMonth = {
    year: rxYear,
    month: rxMonth,
    days: []
  };

  // Process each day of the calendar
  const eventDays = calendarTable.find('td.day-with-date');
  eventDays.each((i, elem) => {
    const dayInMonth = parseInt($(elem).find('.day-number').text());
    const calendarDay: CalendarDay = {
      day: dayInMonth,
      worlds: []
    };
    
    $(elem).find(".calnk-box").each((j, worldElem) => {
      const name = $(worldElem).find('.event-title').text();
      const link = $(worldElem).find('a').attr('href') ?? '';
      calendarDay.worlds.push({
        name: name,
        link: link
      });
    });

    calendarMonth.days.push(calendarDay);
  });

  // Add calendar to cache


  return calendarMonth;
}

const getCalendar = functions.https.onRequest(async(req: functions.https.Request, res : functions.Response) => {
  try
  {
    const current = new Date();

    let year = Number(req.query.year);
    let month = Number(req.query.month);

    if (isNaN(year) || year <= 0) year = current.getFullYear();
    if (isNaN(month) || month <= 0) month = current.getMonth() + 1;

    let calendar: CalendarMonth | null = null;

    const calendarId = String((year * 100) + month);
    const calendarRef = admin.firestore().collection('calendars').doc(calendarId);
    let calendarDoc = await calendarRef.get();
    if (!calendarDoc.exists) {
      calendar = await scrapeCalendar(year, month);
      await calendarRef.set(calendar);
    }
    else {
      calendar = calendarDoc.data() as CalendarMonth;
    }

    if (calendar != null) {
      res.json(calendar);
    } 
    else {
      res.status(500).send("Failed to fetch calendar");
    }
    return;
  }
  catch (e)
  {
    res.status(500).send({ error: e });
  }
  
  res.status(500).send("Unhandled error");
});

export { 
  getCalendar as calendar
};