import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {Observable, of} from 'rxjs';
import {Building} from '../Shared/building';
import {catchError, tap} from 'rxjs/operators';
import {BuildingObject} from '../Shared/building-object';
import {Datafield} from '../Shared/datafield';
import {TimeSeriesDataRequest} from '../Shared/time-series-data-request';

@Injectable({
  providedIn: 'root'
})
export class TimeseriesDataService {

  private httpClient: HttpClient;
  baseUrlLocalhost = 'https://localhost:5001';

  constructor(httpClient: HttpClient) {
    this.httpClient = httpClient;
  }

  public getBuildings(): Observable<Building[]> {
    return this.httpClient.get<Building[]>(`${(this.baseUrlLocalhost)}/building`).pipe(
      tap(_ => this.log('fetched buildings')),
      catchError(this.handleError<Building[]>('getBuildings', []))
    );
  }

  public getObjects(): Observable<BuildingObject[]> {
    return this.httpClient.get<BuildingObject[]>(`${(this.baseUrlLocalhost)}/buildingObject`).pipe(
      tap(_ => this.log('fetched building objects')),
      catchError(this.handleError<BuildingObject[]>('getObjects', []))
    );
  }

  public getDataField(): Observable<Datafield[]> {
    return this.httpClient.get<Datafield[]>(`${(this.baseUrlLocalhost)}/dataField`).pipe(
      tap(_ => this.log('fetched Data Field')),
      catchError(this.handleError<Datafield[]>('getDataField', []))
    );
  }

  public getDateLimit(): Observable<any> {
    return this.httpClient.get<any>(`${(this.baseUrlLocalhost)}/timeseriesData`).pipe(
      tap(_ => this.log('fetched Date Limit')),
      catchError(this.handleError<Datafield[]>('getDateLimit', []))
    );
  }

  public getTimeseriesData(requestObject: TimeSeriesDataRequest): Observable<any[]> {
    return this.httpClient.post<any[]>(`${(this.baseUrlLocalhost)}/timeseriesData`, requestObject).pipe(
      tap(_ => this.log('fetched Data Field')),
      catchError(this.handleError<any[]>('getTimeseriesData', []))
    );
  }

  private handleError<T>(operation = 'operation', result?: T): any {
    return (error: any): Observable<T> => {

      console.error(error); // log to console instead

      this.log(`${operation} failed: ${error.message}`);

      return of(result as T);
    };
  }

  private log(message: string): void {
    console.log(message);
  }
}
