import {Component, OnInit, ViewChild} from '@angular/core';
import {TimeseriesDataService} from './Service/timeseries-data.service';
import {FormControl, FormGroup, Validators} from '@angular/forms';
import {ThemePalette} from '@angular/material/core';
import {TimeSeriesDataRequest} from './Shared/time-series-data-request';

class MatSelectViewData {
  value: number;
  viewValue: string;
}

class TimeseriesViewdata {
  name: string;
  value: number;
}

class BuildingViewdata extends MatSelectViewData {
}

class ObjectViewdata extends MatSelectViewData {
}

class DatafieldViewdata extends MatSelectViewData {
}

@Component({
  selector: 'timeseries-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent implements OnInit {
  title = 'TimeseriesDataWebApp';
  buildingViewdata: BuildingViewdata[] = [];
  objectViewdata: ObjectViewdata[] = [];
  dataFieldViewdata: DatafieldViewdata[] = [];
  startDate: Date; // new Date(2018, 1, 1, 12, 0, 0);
  endDate: Date; // new Date(2020, 1, 1, 12, 0, 0);

  @ViewChild('picker') picker: any;
  @ViewChild('pickerEndDate') pickerEndDate: any;

  selectedStartDate: Date;
  selectedEndDate: Date;
  selectedBuildingId: number;
  selectedObjectId: number;
  selectedDatafieldId: number;

  view: any[] = [700, 300];
  legend = false;
  showLabels = true;
  animations = true;
  xAxis = true;
  yAxis = true;
  showYAxisLabel = true;
  showXAxisLabel = true;
  xAxisLabel = 'Value';
  yAxisLabel = 'Timestamp';
  timeline = true;
  lineChartData: any[];
  colorScheme = {
    domain: ['#5AA454', '#E44D25', '#CFC0BB', '#7aa3e5', '#a8385d', '#aae3f5']
  };

  public date: Date;
  public disabled = false;
  public showSpinners = true;
  public showSeconds = false;
  public touchUi = false;
  public enableMeridian = false;
  public stepHour = 1;
  public stepMinute = 1;
  public stepSecond = 1;
  public color: ThemePalette = 'primary';

  public formGroup = new FormGroup({
    date: new FormControl(null, [Validators.required]),
    date2: new FormControl(null, [Validators.required])
  });
  public dateControl = new FormControl(new Date(2018, 1, 1, 12, 0, 0));
  public dateControlEndDate = new FormControl(new Date(2018, 1, 1, 12, 0, 0));
  public dateControlMinMax = new FormControl(new Date());
  public dateControlMinMaxEndDate = new FormControl(new Date());

  public options = [
    {value: true, label: 'True'},
    {value: false, label: 'False'}
  ];

  public listColors = ['primary', 'accent', 'warn'];

  public stepHours = [1, 2, 3, 4, 5];
  public stepMinutes = [1, 5, 10, 15, 20, 25];
  public stepSeconds = [1, 5, 10, 15, 20, 25];

  private timeseriesService: TimeseriesDataService;

  constructor(timeseriesService: TimeseriesDataService) {
    this.timeseriesService = timeseriesService;
  }

  ngOnInit(): void {
    this.timeseriesService.getBuildings().subscribe(value => {
      const data = new Array<BuildingViewdata>();
      for (const building of value) {
        const item = new BuildingViewdata();
        item.value = building.id;
        item.viewValue = building.name;
        data.push(item);
      }
      this.buildingViewdata.push(...data);
    });
    this.timeseriesService.getObjects().subscribe(value => {
      const data = new Array<ObjectViewdata>();
      for (const building of value) {
        const item = new ObjectViewdata();
        item.value = building.id;
        item.viewValue = building.name;
        data.push(item);
      }
      this.objectViewdata.push(...data);
    });
    this.timeseriesService.getDataField().subscribe(value => {
      const data = new Array<DatafieldViewdata>();
      for (const building of value) {
        const item = new DatafieldViewdata();
        item.value = building.id;
        item.viewValue = building.name;
        data.push(item);
      }
      this.dataFieldViewdata.push(...data);
    });
    this.timeseriesService.getDateLimit().subscribe(value => {
      const item: any = value;
      console.log(item);
      this.startDate = new Date(item.startDate);
      this.endDate = new Date(item.endDate);
      console.log(this.startDate);
      console.log(this.endDate);
    });
  }

  getTimeseriesData(): void {
    const requestObject = new TimeSeriesDataRequest();
    requestObject.StartDate = this.dateControl.value?.toLocaleString();
    requestObject.EndDate = this.dateControlEndDate.value?.toLocaleString();
    requestObject.buildingId = this.selectedBuildingId;
    requestObject.BuildingObjectId = this.selectedObjectId;
    requestObject.DataFieldId = this.selectedDatafieldId;

    if (requestObject.StartDate && requestObject.EndDate && requestObject.buildingId && requestObject.BuildingObjectId && requestObject.DataFieldId) {
      this.timeseriesService.getTimeseriesData(requestObject).subscribe(value => {
        const viewData = [];
        console.log(value);
        for (const timeData of value) {
          const item: any = timeData;
          const date = new Date(item.timestamp.toString());
          viewData.push({name: date.toLocaleString(), value: Number(item.dataValue)});
        }
        this.lineChartData = [{
          name: 'Timeseries Data',
          series: viewData
        }];
      });
    }

  }

  closePicker() {
    this.picker.cancel();
  }

}
