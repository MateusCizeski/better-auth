import { prisma } from "../../config/prisma";
import { CreateUserInput } from "./user.schema";

export const userService = {
    async create(data: CreateUserInput) {
        return prisma.user.create({ data });
    },

    async list() {
         return prisma.user.findMany();
    } 
}