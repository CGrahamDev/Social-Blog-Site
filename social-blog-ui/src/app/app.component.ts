import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { SocialBlogAPIService } from './services/social-blog-api.service';
import { UserDTO } from './DTOs/user-dto';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink, CommonModule, FormsModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'social-blog-ui';
  selectedUser: any;
  selectedUserId: number = -1;
  isLoggedIn: boolean = false;
  constructor(private api : SocialBlogAPIService){};

  getUser(id: number){
    this.api.getUserById(id).subscribe(data => {
      this.selectedUser = data as any;
      this.selectedUserId = this.selectedUser.id;
    });
  }

}
