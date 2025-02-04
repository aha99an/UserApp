import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';

export interface User {
  id: string;
  firstName: string;
  lastName: string;
  email: string;
}

export interface GetUsersQuery {
  Page: number;
  PageSize: number;
  Search?: string;
  SortColumn?: string;
  SortDirection?: string;
}

export interface GetUsersResponse {
  totalCount: number;
  response: User[];
}

export interface ErrorResponse {
  code: string;
  description: string;
  type: number;
  numericType: number;
  metadata: any;
}

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'https://localhost:44374/api/users';

  constructor(private http: HttpClient) { }

  getUsers(query: GetUsersQuery): Observable<GetUsersResponse> {
    let params = new HttpParams()
      .set('Page', query.Page.toString())
      .set('PageSize', query.PageSize.toString());

    if (query.Search) {
      params = params.set('Search', query.Search);
    }
    if (query.SortColumn) {
      params = params.set('SortColumn', query.SortColumn);
    }
    if (query.SortDirection) {
      params = params.set('SortDirection', query.SortDirection);
    }

    return this.http.get<GetUsersResponse>(this.apiUrl, { params });
  }
}