import { prisma } from "../../config/prisma";
import { CreateUserInput } from "./user.schema";

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

    async list() {
         return prisma.users.findMany();
    } 
}