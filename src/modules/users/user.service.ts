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

    async listUsers() {
        const users = await prisma.users.findMany();

        return users;
    },

    async listById({ user_id }: any) {
        const user = await prisma.users.findFirst({
            where: {
                id: user_id
            }
        });

        return user;
    }
}