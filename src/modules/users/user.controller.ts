import { Request, Response, NextFunction } from 'express';
import { createUserSchema } from './user.schema';
import { userService } from './user.service';

export async function createUser(req: Request, res: Response, next: NextFunction) {
    try {
        const data = createUserSchema.parse(req.body);
        const user = await userService.create(data);
        
        res.status(201).json(user);
    } catch(err) {
        next(err);
    }
}

export async function listUsers(req: Request, res: Response, next: NextFunction) {
    try {
        const users = await userService.listUsers();

        res.json(users);
    }catch(err) {
        next(err);
    }
}

export async function listById(req: Request, res: Response, next: NextFunction) {
    try {
        const { user_id } = req.body;
        const user = await userService.listById(user_id);

        res.json(user);
    }catch(err) {
        next(err);
    }
}

export async function updateUser(req: Request, res: Response, next: NextFunction) {
    try {
        const { user_id, name, email } = req.body;
        const user = await userService.updateUser({ user_id, name, email });

        res.json(user);
    }catch(err) {
        next(err);
    }
}

export async function deleteUser(req: Request, res: Response, next: NextFunction) {
    try {
        const { user_id } = req.body;
        
        await userService.deleteUser(user_id);

        res.json();
    }catch(err) {
        next(err);
    }
}