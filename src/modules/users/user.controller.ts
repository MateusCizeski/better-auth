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
        const users = await userService.list();

        res.json(users);
    }catch(err) {
        next(err);
    }
}