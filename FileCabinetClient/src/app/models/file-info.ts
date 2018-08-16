import { TagInfo } from "./tag-info";

export class FileInfo {
    Id: number;
    Name: string;
    UploadDate: string;
    SizeInBytes: string;
    Description: string;
    Tags: TagInfo[]
}