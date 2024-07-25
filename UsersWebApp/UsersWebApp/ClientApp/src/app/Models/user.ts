import { PhoneNumbers } from "./phoneNumber";

export class User {
    userId: number = 0;
    firstName: string = '';
    lastName: string = '';
    phoneNumbers: PhoneNumbers[] = [];
}