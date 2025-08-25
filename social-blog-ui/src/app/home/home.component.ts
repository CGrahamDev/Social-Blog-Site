import { Component, OnInit } from '@angular/core';
import { SocialBlogAPIService } from '../services/social-blog-api.service';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PostDTO } from '../DTOs/post-dto';
import { RouterLink } from '@angular/router';
import { UserDTO } from '../DTOs/user-dto';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule, FormsModule, RouterLink ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css'
})
export class HomeComponent implements OnInit {
    posts: any[] = [];
    users: any[] = [];
    //userToPosts: Record<UserDTO, PostDTO[]>;
    
    selectedPost: any = null;
    constructor(private api: SocialBlogAPIService){};

    ngOnInit(): void {
        this.getPosts();  
    }

    getPosts():void{
      this.api.getPosts().subscribe(data => {
      this.posts = data as any[];
      });
      if(this.posts.length <= 0){
          console.log('successfully recieved posts');
        }else{
          console.log('failed to fetch post information');
    }
  }
  getPostAuthors(postId: number):any{
    this.api.getUserByPost(postId);
  }
}

