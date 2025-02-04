import { Component } from '@angular/core';
import { UserService, GetUsersQuery, GetUsersResponse } from './user.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  users: any[] = [];
  totalCount: number = 0;
  errorMessages: any[] = [];
  currentPage: number = 1;
  pageSize: number = 10;
  searchTerm: string = '';
  totalPages: number = 1;
  sortColumn: string = 'Email'; 
  sortDirection: string = 'asc'; 
  
  constructor(private userService: UserService) {}

  ngOnInit(): void {
    this.fetchUsers();
  }

  fetchUsers(): void {
    const query: GetUsersQuery = {
      Page: this.currentPage,
      PageSize: this.pageSize,
      Search: this.searchTerm,
      SortColumn: this.sortColumn,
      SortDirection: this.sortDirection
    };

    this.userService.getUsers(query).subscribe(
      (response: GetUsersResponse) => {
        this.users = response.response;
        this.totalCount = response.totalCount;
        this.totalPages = Math.ceil(this.totalCount / this.pageSize);
        this.errorMessages = [];
      },
      (error) => {
        this.errorMessages = error.error;
        console.error('Error fetching users:', error);
      }
    );
  }

  onSearch(): void {
    this.currentPage = 1; 
    this.fetchUsers();
  }

  onPageChange(page: number): void {
    this.currentPage = page;
    this.fetchUsers();
  }

  onSortChange(column: string): void {
    if (this.sortColumn === column) {
      this.sortDirection = this.sortDirection === 'asc' ? 'desc' : 'asc';
    } else {
      this.sortColumn = column;
      this.sortDirection = 'asc';
    }

    this.fetchUsers();
  }
}