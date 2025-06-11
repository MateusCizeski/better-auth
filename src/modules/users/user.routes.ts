import { Router } from "express";
import { createUser, listById, listUsers } from "./user.controller";

export const userRoutes = Router();

userRoutes.post('/createUser', createUser);
userRoutes.get('/listUsers', listUsers);
userRoutes.get('/listById', listById);