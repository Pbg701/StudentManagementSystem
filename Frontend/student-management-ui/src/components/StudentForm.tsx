import React, { useState, useEffect } from 'react';
import {
    TextField,
    Button,
    Box,
    Grid
} from '@mui/material';
import { Student } from '../types';

interface StudentFormProps {
    student: Student | null;
    onSubmit: (student: Student) => void;
    onCancel: () => void;
}

const StudentForm: React.FC<StudentFormProps> = ({ student, onSubmit, onCancel }) => {
    const [formData, setFormData] = useState<Omit<Student, 'id' | 'createdDate'>>({
        name: '',
        email: '',
        age: 0,
        course: ''
    });

    useEffect(() => {
        if (student) {
            setFormData({
                name: student.name,
                email: student.email,
                age: student.age,
                course: student.course
            });
        }
    }, [student]);

    const handleChange = (e: React.ChangeEvent<HTMLInputElement>) => {
        const { name, value } = e.target;
        setFormData((prev) => ({
            ...prev,
            [name]: name === 'age' ? parseInt(value) || 0 : value
        }));
    };

    const handleSubmit = (e: React.FormEvent) => {
        e.preventDefault();
        onSubmit({ ...formData, id: student?.id });
    };

    return (
        <form onSubmit={handleSubmit}>
            <Box sx={{ mt: 2 }}>
                <Grid container spacing={2}>
                    <Grid size={{ xs: 12 }}>
                        <TextField
                            fullWidth
                            label="Name"
                            name="name"
                            value={formData.name}
                            onChange={handleChange}
                            required
                        />
                    </Grid>
                    <Grid size={{ xs: 12 }}>
                        <TextField
                            fullWidth
                            label="Email"
                            name="email"
                            type="email"
                            value={formData.email}
                            onChange={handleChange}
                            required
                        />
                    </Grid>
                    <Grid size={{ xs: 6 }}>
                        <TextField
                            fullWidth
                            label="Age"
                            name="age"
                            type="number"
                            value={formData.age}
                            onChange={handleChange}
                            required
                            slotProps={{
                                htmlInput: { min: 1, max: 100 }
                            }}
                        />
                    </Grid>
                    <Grid size={{ xs: 6 }}>
                        <TextField
                            fullWidth
                            label="Course"
                            name="course"
                            value={formData.course}
                            onChange={handleChange}
                            required
                        />
                    </Grid>
                </Grid>
                <Box sx={{ mt: 3, display: 'flex', gap: 2, justifyContent: 'flex-end' }}>
                    <Button onClick={onCancel} variant="outlined">
                        Cancel
                    </Button>
                    <Button type="submit" variant="contained" color="primary">
                        {student ? 'Update' : 'Add'}
                    </Button>
                </Box>
            </Box>
        </form>
    );
};

export default StudentForm;