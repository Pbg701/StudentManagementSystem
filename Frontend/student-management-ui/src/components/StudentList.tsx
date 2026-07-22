import React, { useEffect, useState } from 'react';
import {
    Table,
    TableBody,
    TableCell,
    TableContainer,
    TableHead,
    TableRow,
    Paper,
    Button,
    IconButton,
    Typography,
    Box,
    Dialog,
    DialogActions,
    DialogContent,
    DialogContentText,
    DialogTitle,
    Alert,
    Snackbar,
    CircularProgress
} from '@mui/material';
import { Edit, Delete, Add } from '@mui/icons-material';
import apiClient from '../api/apiClient';
import { Student } from '../types';
import StudentForm from './StudentForm';

const StudentList: React.FC = () => {
    const [students, setStudents] = useState<Student[]>([]);
    const [loading, setLoading] = useState(true);
    const [openDialog, setOpenDialog] = useState(false);
    const [editingStudent, setEditingStudent] = useState<Student | null>(null);
    const [deleteId, setDeleteId] = useState<number | null>(null);
    const [snackbar, setSnackbar] = useState({ open: false, message: '', severity: 'success' as 'success' | 'error' });

    useEffect(() => {
        fetchStudents();
    }, []);

    const fetchStudents = async () => {
        try {
            setLoading(true);
            const data = await apiClient.getStudents();
            console.log('API Response:', data);
            
            let studentsArray = [];
            if (Array.isArray(data)) {
                studentsArray = data;
            } else if (data && typeof data === 'object') {
                if (data.data && Array.isArray(data.data)) {
                    studentsArray = data.data;
                } else if (data.result && Array.isArray(data.result)) {
                    studentsArray = data.result;
                } else if (data.items && Array.isArray(data.items)) {
                    studentsArray = data.items;
                } else if (data.students && Array.isArray(data.students)) {
                    studentsArray = data.students;
                } else {
                    studentsArray = [data];
                }
            }
            
            setStudents(studentsArray);
        } catch (error: any) {
            console.error('Error fetching students:', error);
            showSnackbar(error?.response?.data?.message || 'Failed to fetch students', 'error');
            setStudents([]);
        } finally {
            setLoading(false);
        }
    };

    const handleAdd = () => {
        setEditingStudent(null);
        setOpenDialog(true);
    };

    const handleEdit = (student: Student) => {
        setEditingStudent(student);
        setOpenDialog(true);
    };

    const handleDelete = (id: number) => {
        setDeleteId(id);
    };

    const confirmDelete = async () => {
        if (deleteId) {
            try {
                await apiClient.deleteStudent(deleteId);
                showSnackbar('Student deleted successfully', 'success');
                fetchStudents();
            } catch (error: any) {
                showSnackbar(error?.response?.data?.message || 'Failed to delete student', 'error');
            } finally {
                setDeleteId(null);
            }
        }
    };

    const handleFormSubmit = async (student: Student) => {
        try {
            if (student.id) {
                await apiClient.updateStudent(student.id, student);
                showSnackbar('Student updated successfully', 'success');
            } else {
                await apiClient.createStudent(student);
                showSnackbar('Student added successfully', 'success');
            }
            setOpenDialog(false);
            fetchStudents();
        } catch (error: any) {
            showSnackbar(error?.response?.data?.message || 'Failed to save student', 'error');
        }
    };

    const showSnackbar = (message: string, severity: 'success' | 'error') => {
        setSnackbar({ open: true, message, severity });
    };

    if (loading) {
        return (
            <Box sx={{ display: 'flex', justifyContent: 'center', alignItems: 'center', minHeight: '400px' }}>
                <CircularProgress />
            </Box>
        );
    }

    return (
        <Box sx={{ width: '100%' }}>
            <Box sx={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center', mb: 3 }}>
                <Typography variant="h4">Student Management</Typography>
                <Button
                    variant="contained"
                    color="primary"
                    startIcon={<Add />}
                    onClick={handleAdd}
                >
                    Add Student
                </Button>
            </Box>

            {students.length === 0 ? (
                <Paper sx={{ p: 4, textAlign: 'center' }}>
                    <Typography variant="h6" color="textSecondary">
                        No students found. Click "Add Student" to create one.
                    </Typography>
                </Paper>
            ) : (
                <TableContainer component={Paper}>
                    <Table>
                        <TableHead>
                            <TableRow>
                                <TableCell>ID</TableCell>
                                <TableCell>Name</TableCell>
                                <TableCell>Email</TableCell>
                                <TableCell>Age</TableCell>
                                <TableCell>Course</TableCell>
                                <TableCell>Created Date</TableCell>
                                <TableCell align="center">Actions</TableCell>
                            </TableRow>
                        </TableHead>
                        <TableBody>
                            {students.map((student: Student) => (
                                <TableRow key={student.id}>
                                    <TableCell>{student.id}</TableCell>
                                    <TableCell>{student.name}</TableCell>
                                    <TableCell>{student.email}</TableCell>
                                    <TableCell>{student.age}</TableCell>
                                    <TableCell>{student.course}</TableCell>
                                    <TableCell>
                                        {student.createdDate ? new Date(student.createdDate).toLocaleDateString() : 'N/A'}
                                    </TableCell>
                                    <TableCell align="center">
                                        <IconButton
                                            color="primary"
                                            onClick={() => handleEdit(student)}
                                        >
                                            <Edit />
                                        </IconButton>
                                        <IconButton
                                            color="error"
                                            onClick={() => handleDelete(student.id!)}
                                        >
                                            <Delete />
                                        </IconButton>
                                    </TableCell>
                                </TableRow>
                            ))}
                        </TableBody>
                    </Table>
                </TableContainer>
            )}

            {/* Add/Edit Dialog */}
            <Dialog open={openDialog} onClose={() => setOpenDialog(false)} maxWidth="sm" fullWidth>
                <DialogTitle>{editingStudent ? 'Edit Student' : 'Add Student'}</DialogTitle>
                <DialogContent>
                    <StudentForm
                        student={editingStudent}
                        onSubmit={handleFormSubmit}
                        onCancel={() => setOpenDialog(false)}
                    />
                </DialogContent>
            </Dialog>

            {/* Delete Confirmation Dialog */}
            <Dialog open={!!deleteId} onClose={() => setDeleteId(null)}>
                <DialogTitle>Confirm Delete</DialogTitle>
                <DialogContent>
                    <DialogContentText>
                        Are you sure you want to delete this student? This action cannot be undone.
                    </DialogContentText>
                </DialogContent>
                <DialogActions>
                    <Button onClick={() => setDeleteId(null)}>Cancel</Button>
                    <Button onClick={confirmDelete} color="error" variant="contained">
                        Delete
                    </Button>
                </DialogActions>
            </Dialog>

            {/* Snackbar for notifications */}
            <Snackbar
                open={snackbar.open}
                autoHideDuration={3000}
                onClose={() => setSnackbar({ ...snackbar, open: false })}
                anchorOrigin={{ vertical: 'bottom', horizontal: 'center' }}
            >
                <Alert severity={snackbar.severity} onClose={() => setSnackbar({ ...snackbar, open: false })}>
                    {snackbar.message}
                </Alert>
            </Snackbar>
        </Box>
    );
};

export default StudentList;