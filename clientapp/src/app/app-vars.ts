export class AppVars {
    public RecordStatus = {
        'ACT': 'Active',
        'INACT': 'In Active'
    };

    public PricingTypes = [
        { Value: 25, Description: '25%' },
        { Value: 30, Description: '30%' },
        { Value: 35, Description: '35%' },
        { Value: 40, Description: '40%' },
        { Value: 50, Description: '50%' },
    ];

    public WeekDays: Array<any> = ["Sunday", "Monday", "Tuesday", "Wednesday", "Thursday", "Friday", "Saturday"];
    public WeekDayText(id) {
        if (this.WeekDays[id - 1])
            return this.WeekDays[id - 1];
    }
    constructor() {
    }
}