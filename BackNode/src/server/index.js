import express from 'express';
import { PrismaClient } from '@prisma/client';
import bodyParser from 'body-parser';
import cors from 'cors';

const app = express();
const prisma = new PrismaClient();


app.use(bodyParser.json());
app.use(cors({
  origin: 'http://localhost:5119' 
}));



app.get('/', (req, res) => {
  res.send('Listening...');
});


app.get('/api/tasks', async (req, res) => {
  try {
    const tasks = await prisma.task.findMany();
    res.json(tasks);
  } catch (error) {
    console.error('Error fetching tasks:', error);
    res.status(500).json({ error: 'Error fetching tasks' });
  }
});

app.get('/api/tasks/:id', async (req, res) => {
  const { id } = req.params;
  try {
    const task = await prisma.task.findUnique({
      where: { id: Number(id) },
    });
    
    if (task) {
      res.json(task);
    } else {
      res.status(404).json({ error: 'Task not found' });
    }
  } catch (error) {
    console.error('Error fetching task:', error);
    res.status(500).json({ error: 'Error fetching task' });
  }
});


app.post('/api/tasks', async (req, res) => {
  const { name, description, done, createdAt } = req.body; 
  try {
    const newTask = await prisma.task.create({
      data: {
        name,
        description,
        done,
        createdAt: createdAt ? new Date(createdAt) : undefined, 
      },
    });
    res.json(newTask);
  } catch (error) {
    console.error('Error creating task:', error);
    res.status(500).json({ error: 'Error creating task' });
  }
});


app.put('/api/tasks/:id', async (req, res) => {
  const { id } = req.params;
  const { name, description, done } = req.body;
  try {
    const updatedTask = await prisma.task.update({
      where: { id: Number(id) },
      data: { name, description, done },
    });
    res.json(updatedTask);
  } catch (error) {
    console.error('Error updating task:', error);
    res.status(500).json({ error: 'Error updating task' });
  }
});

app.delete('/api/tasks/:id', async (req, res) => {
  const { id } = req.params;
  try {
    await prisma.task.delete({ where: { id: Number(id) } });
    res.status(204).send();
  } catch (error) {
    console.error('Error deleting task:', error);
    res.status(500).json({ error: 'Error deleting task' });
  }
});

// Start the server
app.listen(3001, () => {
  console.log('Server is running on port 3001');
});
