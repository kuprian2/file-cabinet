using FileCabinet.Dal.Contracts.Domain;
using System;
using System.Collections.Generic;
using System.Data.Entity;

namespace FileCabinet.Dal.EF
{
    public class FileCabinetContextInitializer : DropCreateDatabaseAlways<FileCabinetDbContext>
    {
        //protected override void Seed(FileCabinetDbContext context)
        //{
        //    var tags = new List<Tag>
        //    {
        //        new Tag {Name = "csharp"},
        //        new Tag {Name = "javascript"},
        //        new Tag {Name = "video"},
        //        new Tag {Name = "book"},
        //        new Tag {Name = "forBeginners"},
        //        new Tag {Name = "forAdvanced"}
        //    };

        //    var files = new List<File>
        //    {
        //        new File
        //        {
        //            Name = "Евдокимов П.В. - С# на примерах",
        //            Description = "Эта книга является превосходным учебным пособием для изучения языка " +
        //                          "программирования C# на примерах. Изложение ведется последовательно: " +
        //                          "от развертывания .NET и написания первой программы, до многопоточного " +
        //                          "программирования, создания клиент-серверных приложений и разработки " +
        //                          "программ для мобильных устройств. По ходу даются все необходимые " +
        //                          "пояснения и комментарии. \nКнига написана простым и доступным языком. Лучший " +
        //                          "выбор для результативного изучения C#. Начните сразу писать программы на C#!",
        //            SizeInBytes = 10547039,
        //            UploadDate = DateTime.Now - TimeSpan.FromDays(3),
        //            Url = @"E:\fake\data\way\csharp-na-primerakh.pdf",
        //            Tags = new List<Tag>{ tags[0], tags[3], tags[4] }
        //        },
        //        new File
        //        {
        //            Name = "Рихтер Дж. -  CLR via C#. " +
        //                   "Программирование на платформе Microsoft .NET Framework 4.5" +
        //                   " на языке C# (Мастер-класс) - 2013",
        //            Description = "Эта книга, выходящая в четвертом издании и уже ставшая классическим" +
        //                          " учебником по программированию, подробно описывает внутреннее устройство" +
        //                          " и функционирование общеязыковой исполняющей среды (CLR) " +
        //                          "Microsoft .NET Framework версии 4.5. Написанная признанным экспертом" +
        //                          " в области программирования Джеффри Рихтером, много лет являющимся консультантом" +
        //                          " команды разработчиков .NET Framework компании Microsoft, книга научит вас" +
        //                          " создавать по-настоящему надежные приложения любого вида," +
        //                          " в том числе с использованием Microsoft Silverlight, ASP.NET," +
        //                          " Windows Presentation Foundation и т. д. Четвертое издание полностью" +
        //                          " обновлено в соответствии со спецификацией платформы .NET Framework 4.5," +
        //                          " а также среды Visual Studio 2012 и C# 5.0.",
        //            SizeInBytes = 6569667,
        //            UploadDate = DateTime.Now - TimeSpan.FromDays(13),
        //            Url = @"E:\fake\data\way\clr-via-csharp-rikhter-jeffrey.pdf",
        //            Tags = new List<Tag>{ tags[0], tags[3], tags[5] }
        //        },
        //        new File
        //        {
        //            Name = "C# Tutorial for Beginners",
        //            Description = "1) This is by far the most comprehensive C# course you'll find here," +
        //                          " or anywhere else. \n2) This C# tutorial Series starts from the very" +
        //                          " basics and covers advanced concepts as we progress. This course breaks" +
        //                          " even the most complex applications down into simplistic steps. \n" +
        //                          "3) It is aimed at complete beginners, and assumes that you have no" +
        //                          " programming experience whatsoever. \n4) This C# tutorial Series uses" +
        //                          " Visual training method, offering users increased retention and" +
        //                          " accelerated learning. \n5) You don't need to buy any software for" +
        //                          " this course! You can use the free Visual Studio Express Edition from Microsoft." +
        //                          " \n6) This course focuses on the language, and not the graphical" +
        //                          " aspects of windows programming. \nTake the first step and start your" +
        //                          " programming career now. \nWhat are the requirements? \nThe course is aimed" +
        //                          " to teach you C#, whether you are an experienced programmer or just getting" +
        //                          " started \nWhat am I going to get from this course? \nUse and understand variables" +
        //                          " \nBy the end of this course, you should definitely be able to" +
        //                          " understand and write good C# code. \nLearn about core programming concepts" +
        //                          " \nWork with Classes and Objects \nWhat is the target audience? \n" +
        //                          "A basic knowledge of programming is helpful but not necessary to get the most" +
        //                          " out of this course \nA genuine interest to learn.",
        //            SizeInBytes = 254108844,
        //            UploadDate = DateTime.Now - TimeSpan.FromDays(535),
        //            Url = @"E:\fake\data\way\csharp-tutorial-forbeginners.mp4",
        //            Tags = new List<Tag>{ tags[0], tags[2], tags[4] }
        //        },
        //        new File
        //        {
        //            Name = "JavaScript for Kids: A Playful Introduction to Programming by Nick Morgan",
        //            Description = "True to the title, this book is a whimsical exploration of very" +
        //                          " basic programming concepts, but don’t let that fool you. Books" +
        //                          " for kids aren’t just for kids. If you have never touched code before," +
        //                          " this is a good place to start, even if you’re all grown up." +
        //                          " Diving in the deep end before you learn how to swim can be a" +
        //                          " frustrating experience. It’s better to start your practice with" +
        //                          " some easy wins.",
        //            SizeInBytes = 1683564,
        //            UploadDate = DateTime.Now - TimeSpan.FromHours(14),
        //            Url = @"E:\fake\data\way\javascript-for-kids.pdf",
        //            Tags = new List<Tag>{ tags[1], tags[3], tags[4] }
        //        },
        //        new File
        //        {
        //            Name = "“Eloquent JavaScript: A Modern Introduction to Programming” by Marijn Haverbeke",
        //            Description = "This book is a work of art. It walks you through the essential concepts" +
        //                          " with a clear roadmap using clear language. It’s masterfully composed and" +
        //                          " edited, and unlike most programming books, it’s full of" +
        //                          " exercises for you to practice. If I were teaching the basics of" +
        //                          " programming in high school or college, I would use this as a text book.",
        //            SizeInBytes = 3450571,
        //            UploadDate = DateTime.Now - TimeSpan.FromDays(512),
        //            Url = @"E:\fake\data\way\eloquent-javascript.docx",
        //            Tags = new List<Tag>{ tags[1], tags[3], tags[5] }
        //        },
        //        new File
        //        {
        //            Name = "Курс javascript essential (ITVDN)",
        //            Description = "",
        //            SizeInBytes = 7093451,
        //            UploadDate = DateTime.Now - TimeSpan.FromDays(11),
        //            Url = @"E:\fake\data\way\javascript-essential-itvdn.mp4",
        //            Tags = new List<Tag>{ tags[1], tags[2], tags[4] }
        //        }
        //    };

        //    var users = new List<User>
        //    {
        //        new User
        //        {
        //            Name = "Alex",
        //            Bookmarks = new List<File> {files[1], files[3]},
        //        },
        //        new User
        //        {
        //            Name = "Jeffrey",
        //            Bookmarks = new List<File> {files[2], files[4], files[3]},
        //        },
        //        new User
        //        {
        //            Name = "Chris",
        //            Bookmarks = new List<File> {files[2], files[5]},
        //        }
        //    };

        //    context.Tags.AddRange(tags);
        //    context.Files.AddRange(files);
        //    context.Users.AddRange(users);

        //    context.SaveChanges();
        //}
    }
}
