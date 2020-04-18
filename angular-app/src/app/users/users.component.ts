import { Component, OnInit } from '@angular/core';
import { User } from '../user';
import { USERS } from '../mock-users';

@Component({
  selector: 'app-users',
  templateUrl: './users.component.html',
  styleUrls: ['./users.component.css']
})
export class UsersComponent implements OnInit {

  user: User = {
    id: 1,
    name: 'Harold'
  };

  users = USERS;

  constructor() { }

  ngOnInit(): void {
  }

  selectedUser: User;
  onSelect(user: User): void {
    this.selectedUser = user;
}

}
