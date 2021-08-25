import { Role } from "./role";


export class User {
  public id!: number;
  public login!: string;
  public name!: string;
  public email!: string;
  public roles!: Role[];
}
