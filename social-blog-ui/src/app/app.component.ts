import { CommonModule } from '@angular/common';
import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { SocialBlogAPIService } from './services/social-blog-api.service';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, RouterLink, CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'social-blog-ui';
  currentUserId: number = -1;

  constructor(private api : SocialBlogAPIService){};

  getUser(){

  }

}
