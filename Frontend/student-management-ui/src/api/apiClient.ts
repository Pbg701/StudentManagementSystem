import axios, { AxiosInstance, AxiosError } from 'axios';
import { Student } from '../types';

// Use the correct URL from your .NET API
const API_BASE_URL = 'https://localhost:7133'; // Change this to match your API port

class ApiClient {
    private client: AxiosInstance;

    constructor() {
        this.client = axios.create({
            baseURL: API_BASE_URL,
            headers: {
                'Content-Type': 'application/json',
            },
        });

        // Add token to requests
        this.client.interceptors.request.use(
            (config) => {
                const token = localStorage.getItem('token');
                if (token) {
                    config.headers.Authorization = `Bearer ${token}`;
                }
                return config;
            },
            (error) => Promise.reject(error)
        );

        // Handle global errors
        this.client.interceptors.response.use(
            (response) => {
                console.log('API Response:', response.data); // Debug log
                return response;
            },
            (error: AxiosError) => {
                console.error('API Error:', error);
                if (error.response?.status === 401) {
                    localStorage.removeItem('token');
                    window.location.href = '/login';
                }
                return Promise.reject(error);
            }
        );
    }

    // Auth endpoints
    login = async (username: string, password: string) => {
        const response = await this.client.post('/api/Auth/login', { username, password });
        return response.data;
    };

    // Student endpoints
    getStudents = async () => {
        const response = await this.client.get('/api/students');
        // Return the data directly, handling will be done in component
        return response.data;
    };

    createStudent = async (student: Omit<Student, 'id' | 'createdDate'>) => {
        const response = await this.client.post('/api/students', student);
        return response.data;
    };

    updateStudent = async (id: number, student: Partial<Student>) => {
        const response = await this.client.put(`/api/students/${id}`, student);
        return response.data;
    };

    deleteStudent = async (id: number) => {
        const response = await this.client.delete(`/api/students/${id}`);
        return response.data;
    };
}

export default new ApiClient();