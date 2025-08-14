export interface PostDTO {
    id: number | null;
    title: string;
    content: string;
    authorId: number;
    comments: number | null;
}
//NOTE: DTOs HAVE TO BE EQUIVALENT TO THE JSON INFORMATION BEING RECEIVED BY THE API