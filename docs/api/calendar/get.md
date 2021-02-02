# Get Calendar

Get the zwift guest worlds for the specifed month.
- **Method**: `GET`
- **URL**: `/api/v1/calendar?year={year}&month={month}`
- **Query Paramenters**:

| Parameter | Required | Description |
| ------------ | ------------ | ------------ |
| `year` | No | The year the fetch the month of. Defaults to the current year. | 
| `month` | No | The month of the year to fetch the calendar of (1-indexed i.e. Jan = 1, Feb = 2...). Defaults to the current month. | 

## Response
- **Code**: `200`
- **Example Content**:
```json
{
	"year": 2021,
	"month": 2,
	"days": [
		{
			"day": 1,
			"worlds": [
				{
					"name": "London",
					"link": "https://zwiftinsider.com/london"
				},
				{
					"name": "Yorkshire",
					"link": "https://zwiftinsider.com/yorkshire"
				}
			]
		},
		// ...
	]
}
```