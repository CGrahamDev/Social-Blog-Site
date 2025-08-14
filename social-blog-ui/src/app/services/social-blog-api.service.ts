import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { UserDTO } from '../DTOs/user-dto';
import { PostDTO } from '../DTOs/post-dto';
import { CommentDTO } from '../DTOs/comment-dto';

@Injectable({
  providedIn: 'root'
})
export class SocialBlogAPIService {
  baseUrl : string  = 'https://localhost:7017/api';
  constructor(private http: HttpClient) { }
  //Users
  getUsers(){
    return this.http.get(`${this.baseUrl}/Users`);
  }
  getUserById(id: number){
    return this.http.get(`${this.baseUrl}/Users/${id}`);
  }
  getUserByPost(postId: number){
    return this.http.get(`${this.baseUrl}/Users/post/${postId}`);    
  }
  editUser(id: number, user: UserDTO){
    return this.http.put(`${this.baseUrl}/Users/${id}`, user);
  }
  createUser(user: UserDTO){
    return this.http.post(`${this.baseUrl}/Users`, user);
  }
  deleteUser(id: number){
    return this.http.delete(`${this.baseUrl}/Users/${id}`);
  }


  //Posts
  getPosts(){
    return this.http.get(`${this.baseUrl}/Posts`);
  }
  getPostById(id: number){
    return this.http.get(`${this.baseUrl}/Posts/${id}`);
  }
  getPostsByUserId(userId: number){
    return this.http.get(`${this.baseUrl}/Posts/user/${userId}`);
  }
  getPostByCommentId(commentId: number){
    return this.http.get(`${this.baseUrl}/Posts/comment/${commentId}`);
  }
  createPost(post: PostDTO){
    return this.http.post(`${this.baseUrl}/Posts`, post);
  }
  editPost(id: number, post: PostDTO){
    return this.http.put(`${this.baseUrl}/Posts/${id}`, post );
  }
  deletePost(id: number){
    return this.http.delete(`${this.baseUrl}/Posts/${id}`);
  }

  //Comments
  getComments(){
    return this.http.get(`${this.baseUrl}/Comments`);
  }
  getCommentsByPostId(postId: number){
    return this.http.get(`${this.baseUrl}/Comments/post/${postId}`);
  }
  getCommentsByUserId(userId: number){
    return this.http.get(`${this.baseUrl}/Comments/user/${userId}`);
  }
  createComment(comment: CommentDTO){
    return this.http.post(`${this.baseUrl}/Comments`, comment);
  }  
  editComment(id: number, comment: CommentDTO){
    return this.http.put(`${this.baseUrl}/Comments/${id}`, comment);
  }
  deleteComment(id: number){
    return this.http.delete(`${this.baseUrl}/Comments/${id}`);
  }


}
