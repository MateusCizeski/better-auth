import express from "express";
import { userRoutes } from "./modules/users/user.routes";

export const app = express();

app.use(express.json());
app.use('/users', userRoutes);