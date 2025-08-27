import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { SocialBlogAPIService } from '../services/social-blog-api.service';
import { PostDTO } from '../DTOs/post-dto';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-post',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './post.component.html',
  styleUrl: './post.component.css'
})
export class PostComponent implements OnInit {
  post: any;
  comments: any[] = [];
  activatedRouteParameter: any = -1;
  constructor(private api:SocialBlogAPIService, private route: ActivatedRoute){}
  
  ngOnInit(): void {
      this.activatedRouteParameter = this.route.snapshot.url[0].parameters[0];
      this.getPostByUrl(this.activatedRouteParameter);
  }
  getPostByUrl(id: number){

    this.api.getPostById(id).subscribe(data => {
      this.post = data as any;
    });
  }

}

