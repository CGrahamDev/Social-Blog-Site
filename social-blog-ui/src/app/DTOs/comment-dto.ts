export interface CommentDTO {
    id: number | null;
    content: string;
    likes: number;
    userId: number;
    postId: number;
}
