export interface Student {
    id?: number;
    name: string;
    email: string;
    age: number;
    course: string;
    createdDate?: string;
}

export interface LoginRequest {
    username: string;
    password: string;
}

export interface LoginResponse {
    token: string;
}