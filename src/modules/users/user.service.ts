import { prisma } from "../../config/prisma";
import { CreateUserInput } from "./user.schema";

interface UpdateUserProp {
    user_id: string;
    name: string;
    email: string;
}

export const userService = {
    async create(data: CreateUserInput) {
        const user = await prisma.users.create({
            data: {
                name: data.name,
                email: data.email
            }
        });

        return user;
    },

    async listUsers() {
        const users = await prisma.users.findMany();

        return users;
    },

    async listById(user_id: string) {        
        const user = await prisma.users.findFirst({
            where: {
                id: user_id
            }
        });

        return user;
    },

    async updateUser({  user_id, name, email}: UpdateUserProp) {
        const userAlreadyExists = await prisma.users.findFirst({
            where: {
                id: user_id
            }
        });

        if(!userAlreadyExists) {
            throw new Error("User not exists.");
        }

        const userUpdate = await prisma.users.update({
            where: {
                id: user_id
            },
            data: {
                name,
                email
            }
        });
        
        return userUpdate;
    },

    async deleteUser(user_id: string) {
        await prisma.users.delete({
            where: {
                id: user_id
            }
        });
    }
}