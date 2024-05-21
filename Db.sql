
CREATE DATABASE PetHealthCareSystem;
GO
USE PetHealthCareSystem;

GO
IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

CREATE TABLE [Cages] (
    [CageId] int NOT NULL IDENTITY,
    [IsOccupied] bit NOT NULL,
    [CageNumber] int NOT NULL,
    CONSTRAINT [PK_Cages] PRIMARY KEY ([CageId])
);
GO

INSERT INTO [dbo].[Cages] (CageNumber, isOccupied)
VALUES
    (1, 0),
    (2, 1),
    (3, 0),
    (4, 1),
    (5, 0),
    (6, 1),
    (7, 0),
    (8, 1),
    (9, 0),
    (10, 1),
    (11, 0),
    (12, 1),
    (13, 0),
    (14, 1),
    (15, 0),
    (16, 1),
    (17, 0),
    (18, 1),
    (19, 0),
    (20, 1);

GO

CREATE TABLE [Roles] (
    [RoleId] int NOT NULL IDENTITY,
    [RoleName] nvarchar(10) NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([RoleId])
);
GO

INSERT INTO [dbo].[Roles] (RoleName)
VALUES ( N'Customer'),
       ( N'Admin'),
       ( N'Vet'),
       (N'Staff');

GO

CREATE TABLE [Services] (
    [ServiceId] int NOT NULL IDENTITY,
    [ServicePrice] float NOT NULL,
    [ServiceName] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Services] PRIMARY KEY ([ServiceId])
);
GO

INSERT INTO [dbo].[Services] (ServiceName, ServicePrice)
VALUES
    ('Vaccinations', 50),
    ('Dental care', 300),
    ('Spaying and neutering', 250),
    ('Diagnostic testing', 100),
    ('Surgery', 1000),
    ('Nutritional counselling', 70),
    ('Emergency and critical care', 1000);

GO

CREATE TABLE [TimeSlots] (
    [TimeSlotId] int NOT NULL IDENTITY,
    [StartTime] time NOT NULL,
    [EndTime] time NOT NULL,
    CONSTRAINT [PK_TimeSlots] PRIMARY KEY ([TimeSlotId])
);
GO

INSERT INTO [dbo].[TimeSlots] 
    (StartTime, EndTime)
VALUES
    ('07:00', '08:30'),
    ('08:30', '10:00'),
    ('10:00', '11:30'),
    ('13:00', '14:30'),
    ('14:30', '16:00'),
    ('16:00', '17:30');

GO
CREATE TABLE [Accounts] (
    [AccountId] char(10) NOT NULL,
    [Username] nvarchar(20) NOT NULL,
    [FullName] nvarchar(50) NOT NULL,
    [Password] nvarchar(16) NOT NULL,
    [IsMale] bit NOT NULL,
    [PhoneNumber] nvarchar(max) NOT NULL,
    [Email] nvarchar(max) NOT NULL,
    [DateOfBirth] date NOT NULL,
    [JoinDate] date NOT NULL,
    [IsDisabled] bit NOT NULL,
    [RoleId] int NOT NULL,
    [Discriminator] nvarchar(13) NOT NULL,
    [ImgUrl] nvarchar(max) NULL,
    [Experience] int NULL,
    [Description] nvarchar(200) NULL,
    [Position] nvarchar(30) NULL,
    [Department] nvarchar(50) NULL,
    CONSTRAINT [PK_Accounts] PRIMARY KEY ([AccountId]),
    CONSTRAINT [FK_Accounts_Roles_RoleId] FOREIGN KEY ([RoleId]) REFERENCES [Roles] ([RoleId]) ON DELETE CASCADE
);
GO

INSERT INTO [dbo].[Accounts] 
    (AccountId, Username, FullName, Password, IsMale, PhoneNumber, Email, DateOfBirth, JoinDate, IsDisabled, RoleId, Discriminator, ImgUrl, Experience, Description, Position, Department)
VALUES
    ('VE00000000', 'thegoat3000', 'Tony Stark', '123456789', 1, '0937661777', 'tonystark@gmail.com', '1939-03-12', '2000-05-20', 0, 3, 'Veterinarian', 'https://sjkdlfj234.com/oij23lka/sdljf234jlk.html', 10, 'Handsome and straight', 'Senior', 'Surgery'),
    ('AC00000001', 'user1', 'Gojo Satoru', '123456789', 1, '0937661771', 'user1@gmail.com', '1926-03-01', '2024-05-20', 0, 1, 'customer', null, null, null, null, null),
    ('AC00000002', 'admin', 'John Wick', '123456789', 0, '0937661777', 'admin@gmail.com', '1929-03-01', '1960-05-20', 0, 2, 'admin', null, null, null, null, null),
    ('AC00000003', 'user2', 'Nick Furry', '123456789', 1, '0937661777', 'user2@gmail.com', '1984-01-01', '2000-05-20', 0, 1, 'customer', null, null, null, null, null),
    ('VE00000004', 'vet3', 'Peter Parker', '123456789', 0, '0937661777', 'vet3@gmail.com', '1984-01-01', '1999-05-20', 0, 3, 'Veterinarian', 'https://sjkdlfj234.com/oij23lka/sdljf234jlk.html', 10, 'Beautiful but gay', 'Junior', 'Surgery');

GO

CREATE TABLE [Feedbacks] (
    [FeedbackId] int NOT NULL IDENTITY,
    [Ratings] int NOT NULL,
    [FeedbackDetails] nvarchar(250) NULL,
    [AccountId] char(10) NULL,
    CONSTRAINT [PK_Feedbacks] PRIMARY KEY ([FeedbackId]),
    CONSTRAINT [FK_Feedbacks_Accounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Accounts] ([AccountId])
);
GO

INSERT INTO [dbo].[Feedbacks] 
    (AccountId, Ratings, [FeedbackDetails])
VALUES
    ('AC00000001', 4, 'gud doctor'),
    ('AC00000001', 2, 'gud doctor'),
    ('AC00000001', 5, 'bad doctor'),
    ('AC00000003', 1, 'very gud doctor'),
    ('AC00000003', 2, 'very gud doctor');

GO
CREATE TABLE [Pets] (
    [PetId] char(10) NOT NULL,
    [ImgUrl] nvarchar(max) NOT NULL,
    [PetName] nvarchar(50) NOT NULL,
    [PetBreed] nvarchar(50) NOT NULL,
    [PetAge] int NOT NULL,
    [Description] nvarchar(100) NULL,
    [IsMale] bit NOT NULL,
    [IsCat] bit NOT NULL,
    [VaccinationHistory] nvarchar(200) NULL,
    [IsDisabled] bit NOT NULL,
    [AccountId] char(10) NOT NULL,
    CONSTRAINT [PK_Pets] PRIMARY KEY ([PetId]),
    CONSTRAINT [FK_Pets_Accounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Accounts] ([AccountId]) ON DELETE NO ACTION
);
GO

INSERT INTO [dbo].[Pets] (PetId, ImgUrl, PetName, PetBreed, PetAge, Description, IsMale, IsCat, VaccinationHistory, IsDisabled, AccountId)
VALUES ('PE00000001', N'https://example.com/pet1.jpg', N'Buddy', N'Golden Retriever', 3, N'Friendly and active', 1, 0, N'Rabies, Distemper', 0, 'AC00000003'),
       ('PE00000002', N'https://example.com/pet2.jpg', N'Mittens', N'Siamese', 2, N'Quiet and affectionate', 0, 1, N'Rabies, Feline Leukemia', 0, 'AC00000003'),
       ('PE00000003', N'https://example.com/pet3.jpg', N'Rex', N'German Shepherd', 5, N'Loyal and protective', 1, 0, N'Rabies, Distemper, Parvovirus', 1, 'AC00000003'),
       ('PE00000004', N'https://example.com/pet4.jpg', N'Bella', N'Bulldog', 4, N'Gentle and calm', 0, 0, N'Rabies, Parvovirus', 0, 'AC00000001'),
       ('PE00000005', N'https://example.com/pet5.jpg', N'Simba', N'Maine Coon', 1, N'Playful and sociable', 1, 1, N'Rabies, Feline Leukemia, FIV', 0, 'AC00000001');

GO

CREATE TABLE [Appointments] (
    [AppointmentId] char(10) NOT NULL,
    [AppointmentDate] date NOT NULL,
    [AppointmentType] nvarchar(50) NOT NULL,
    [AppointmentNotes] nvarchar(200) NOT NULL,
    [BookingPrice] float NOT NULL,
    [AccountId] char(10) NOT NULL,
    [PetId] char(10) NOT NULL,
    [VeterinarianAccountId] char(10) NOT NULL,
    [TimeSlotId] int NOT NULL,
    CONSTRAINT [PK_Appointments] PRIMARY KEY ([AppointmentId]),
    CONSTRAINT [FK_Appointments_Accounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Accounts] ([AccountId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Appointments_Accounts_VeterinarianAccountId] FOREIGN KEY ([VeterinarianAccountId]) REFERENCES [Accounts] ([AccountId]) ON DELETE CASCADE,
    CONSTRAINT [FK_Appointments_Pets_PetId] FOREIGN KEY ([PetId]) REFERENCES [Pets] ([PetId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_Appointments_TimeSlots_TimeSlotId] FOREIGN KEY ([TimeSlotId]) REFERENCES [TimeSlots] ([TimeSlotId]) ON DELETE CASCADE
);
GO
INSERT INTO [dbo].[Appointments] (AppointmentId, AccountId, AppointmentDate, PetId, VeterinarianAccountId, TimeSlotId, AppointmentType, AppointmentNotes, BookingPrice)
VALUES
    ('AP00000001', 'AC00000001', '2024-03-24', 'PE00000001', 'VE00000000', 1, 'Deposit', 'My pet is sneezing and watery eyes', 500000),
    ('AP00000002', 'AC00000001', '2024-04-27', 'PE00000002', 'VE00000004', 2, 'Deposit', 'My cat loss of appetite', 700000),
    ('AP00000003', 'AC00000001', '2024-05-12', 'PE00000003', 'VE00000000', 3, 'Deposit', 'I think my pet is stressed', 100000),
    ('AP00000004', 'AC00000003', '2024-05-15', 'PE00000004', 'VE00000004', 4, 'Deposit', 'Scratching, skin irritation', 1000000),
    ('AP00000005', 'AC00000003', '2024-05-26', 'PE00000005', 'VE00000000', 5, 'Deposit', 'Irregular bathroom habits, dehydration', 1000000);
GO

CREATE TABLE [BookingPayments] (
    [PaymentId] char(10) NOT NULL,
    [Price] float NOT NULL,
    [PaymentMethod] nvarchar(50) NOT NULL,
    [PaymentDate] date NOT NULL,
    [AppointmentId] char(10) NULL,
    CONSTRAINT [PK_BookingPayments] PRIMARY KEY ([PaymentId]),
    CONSTRAINT [FK_BookingPayments_Appointments_AppointmentId] FOREIGN KEY ([AppointmentId]) REFERENCES [Appointments] ([AppointmentId])
);
GO

INSERT INTO [dbo].[BookingPayments] (PaymentId, Price, PaymentMethod, PaymentDate, AppointmentId)
VALUES
    ('BP00000001', 500000, 'Momo', '2024-03-22', 'AP00000001'),
	('BP00000002', 1000000, 'BankTransfer', '2024-04-27', 'AP00000002'),
	('BP00000003', 500000, 'Cash', '2024-05-12', 'AP00000001'),
	('BP00000004', 1000000, 'Cash', '2024-05-15', 'AP00000004'),
	('BP00000005', 1000000, 'Cash', '2024-05-26', 'AP00000005'),
	('BP00000006', 1000000, 'Cash', '2024-05-26', 'AP00000003');

GO
CREATE TABLE [MedicalRecords] (
    [MedicalRecordId] char(10) NOT NULL,
    [DateCreated] date NOT NULL,
    [PetWeight] int NOT NULL,
    [Symptoms] nvarchar(200) NULL,
    [Allergies] nvarchar(200) NULL,
    [Diagnosis] nvarchar(200) NULL,
    [AdditionalNotes] nvarchar(300) NULL,
    [FollowUpAppointmentDate] date NULL,
    [FollowUpAppointmentNotes] nvarchar(300) NULL,
    [DrugPrescriptions] nvarchar(500) NULL,
    [AppointmentId] char(10) NOT NULL,
    [PetId] char(10) NOT NULL,
    CONSTRAINT [PK_MedicalRecords] PRIMARY KEY ([MedicalRecordId]),
    CONSTRAINT [FK_MedicalRecords_Appointments_AppointmentId] FOREIGN KEY ([AppointmentId]) REFERENCES [Appointments] ([AppointmentId]) ON DELETE CASCADE,
    CONSTRAINT [FK_MedicalRecords_Pets_PetId] FOREIGN KEY ([PetId]) REFERENCES [Pets] ([PetId]) ON DELETE NO ACTION
);
GO

INSERT INTO [dbo].[MedicalRecords] 
    (MedicalRecordID, DateCreated, PetID, AppointmentID, PetWeight, Symptoms, Allergies, Diagnosis, AdditionalNotes, FollowUpAppointmentDate, FollowUpAppointmentNotes, DrugPrescriptions)
VALUES
    ('ME00000001', '2024-03-24', 'PE00000001', 'AP00000001', 3, 'occasional sneezing, watery eyes', 'Not Known', 'mild allergic reaction to pollen', 'Suggested monitoring outdoor activity during high pollen count days.', '2024-03-27', 'Evaluate allergy response.', 'Antihistamines'),
    ('ME00000002', '2024-04-27', 'PE00000002', 'AP00000002', 3.5, 'lethargy, loss of appetite', 'Not Known', 'gastritis', 'Keep on a bland diet, small servings until symptoms improve.', '2024-04-30', 'Check-up on recovery progress.', 'Probiotics, antacid medication.'),
    ('ME00000003', '2024-05-12', 'PE00000003', 'AP00000003', 2.5, 'irregular grooming, stress behavior', 'Not Known', 'anxiety', 'Advised environmental changes and stress relief techniques.', '2024-05-15', 'Assess anxiety levels and effectiveness of interventions.', 'Mild sedative for high-stress situations.'),
    ('ME00000004', '2024-05-15', 'PE00000004', 'AP00000004', 4, 'scratching, skin irritation', 'possible reaction to new dog food brand', 'dermatitis', 'Switched back to original dog food brand; use medicated shampoo for 2 weeks', '2024-05-18', 'Skin condition review.', 'Topical corticosteroid cream for affected areas.'),
    ('ME00000005', '2024-05-26', 'PE00000005', 'AP00000005', 4, 'irregular bathroom habits, dehydration', 'Not Known', 'early-stage kidney disease', 'Hydration is critical; recommend wet food diet and regular water intake.', '2024-05-29', 'Review kidney function and adapt diet if needed.', 'Subcutaneous fluids as needed, renal support diet.');

GO
CREATE TABLE [AdmissionRecords] (
    [AdmissionId] char(10) NOT NULL,
    [AdmissionDate] date NOT NULL,
    [DischargeDate] date NULL,
    [IsDischarged] bit NOT NULL,
    [PetCurrentCondition] nvarchar(50) NULL,
    [MedicalRecordId] char(10) NOT NULL,
    [VeterinarianAccountId] char(10) NOT NULL,
    [PetId] char(10) NOT NULL,
    [CageId] int NOT NULL,
    CONSTRAINT [PK_AdmissionRecords] PRIMARY KEY ([AdmissionId]),
    CONSTRAINT [FK_AdmissionRecords_Accounts_VeterinarianAccountId] FOREIGN KEY ([VeterinarianAccountId]) REFERENCES [Accounts] ([AccountId]) ON DELETE CASCADE,
    CONSTRAINT [FK_AdmissionRecords_Cages_CageId] FOREIGN KEY ([CageId]) REFERENCES [Cages] ([CageId]) ON DELETE CASCADE,
    CONSTRAINT [FK_AdmissionRecords_MedicalRecords_MedicalRecordId] FOREIGN KEY ([MedicalRecordId]) REFERENCES [MedicalRecords] ([MedicalRecordId]) ON DELETE NO ACTION,
    CONSTRAINT [FK_AdmissionRecords_Pets_PetId] FOREIGN KEY ([PetId]) REFERENCES [Pets] ([PetId]) ON DELETE NO ACTION
);
GO

INSERT INTO [dbo].[AdmissionRecords] (AdmissionId, AdmissionDate, DischargeDate, IsDischarged, PetCurrentCondition, MedicalRecordId, VeterinarianAccountId, PetId, CageId)
VALUES
    ('AR00000001', '2023-05-16', '2023-05-20', 1, N'Đã xuất viện', 'ME00000001', 'VE00000000', 'PE00000001', 1),
    ('AR00000002', '2024-05-13', NULL, 0, N'Đang ngủ', 'ME00000002', 'VE00000000', 'PE00000002', 3),
    ('AR00000003', '2024-05-18', '2024-05-20', 1, N'Đã xuất viện', 'ME00000003', 'VE00000000', 'PE00000003', 4),
    ('AR00000004', '2024-02-13', '2024-02-16', 1, N'Đã xuất viện', 'ME00000004', 'VE00000000', 'PE00000004', 5),
    ('AR00000005', '2024-05-20', NULL, 0, N'Đã ăn trưa', 'ME00000005', 'VE00000004', 'PE00000005', 7);


CREATE TABLE [ServiceOrders] (
    [ServiceOrderId] char(10) NOT NULL,
    [Price] float NOT NULL,
    [OrderDate] date NOT NULL,
    [OrderStatus] nvarchar(50) NOT NULL,
    [MedicalRecordId] char(10) NOT NULL,
    CONSTRAINT [PK_ServiceOrders] PRIMARY KEY ([ServiceOrderId]),
    CONSTRAINT [FK_ServiceOrders_MedicalRecords_MedicalRecordId] FOREIGN KEY ([MedicalRecordId]) REFERENCES [MedicalRecords] ([MedicalRecordId]) ON DELETE CASCADE
);
GO
INSERT INTO [dbo].[ServiceOrders] (ServiceOrderId, Price, OrderDate, OrderStatus, MedicalRecordId)
VALUES 
('SR00000001', 350, '2024-03-24', 'Paid', 'ME00000001'),
('SR00000002', 550, '2024-04-27', 'Paid', 'ME00000002'),
('SR00000003', 1100, '2024-05-12', 'Paid', 'ME00000003'),
('SR00000004', 70, '2024-05-15', 'Paid', 'ME00000004'),
('SR00000005', 1000, '2024-05-26', 'Paid', 'ME00000005');

GO
CREATE TABLE [PetHealthTracker] (
    [PetHealthTrackerId] char(10) NOT NULL,
    [PetName] nvarchar(50) NOT NULL,
    [TrackerDate] date NOT NULL,
    [Description] nvarchar(max) NOT NULL,
    [PetId] char(10) NOT NULL,
    [AdmissionRecordAdmissionId] char(10) NOT NULL,
    CONSTRAINT [PK_PetHealthTracker] PRIMARY KEY ([PetHealthTrackerId]),
    CONSTRAINT [FK_PetHealthTracker_AdmissionRecords_AdmissionRecordAdmissionId] FOREIGN KEY ([AdmissionRecordAdmissionId]) REFERENCES [AdmissionRecords] ([AdmissionId]) ON DELETE CASCADE,
    CONSTRAINT [FK_PetHealthTracker_Pets_PetId] FOREIGN KEY ([PetId]) REFERENCES [Pets] ([PetId]) ON DELETE CASCADE
);
GO

INSERT INTO [dbo].[PetHealthTracker] (PetHealthTrackerId, PetName, TrackerDate, Description, PetId, AdmissionRecordAdmissionId)
VALUES 
('PT0000001', 'Buddy', '2024-03-25', 'The pet is progress in a positive way', 'PE00000001', 'AR00000001'),
('PT0000002', 'Mittens', '2024-04-28', 'Condition is getting worse', 'PE00000002', 'AR00000002'),
('PT0000003', 'Rex', '2024-05-13', 'Feed the pet at 3 a.m', 'PE00000003', 'AR00000003'),
('PT0000004', 'Bella', '2024-05-16', 'The pet is fully recoverd', 'PE00000005', 'AR00000004'),
('PT0000005', 'Simba', '2024-05-27', 'The pet is hard to sleep', 'PE00000004', 'AR00000005');

GO
CREATE TABLE [ServicePayments] (
    [ServicePaymentId] char(10) NOT NULL,
    [ServicePrice] float NOT NULL,
    [PaymentDate] date NOT NULL,
    [PaymentMethod] nvarchar(20) NOT NULL,
    [ServiceOrderId] char(10) NOT NULL,
    CONSTRAINT [PK_ServicePayments] PRIMARY KEY ([ServicePaymentId]),
    CONSTRAINT [FK_ServicePayments_ServiceOrders_ServiceOrderId] FOREIGN KEY ([ServiceOrderId]) REFERENCES [ServiceOrders] ([ServiceOrderId]) ON DELETE CASCADE
);
GO

INSERT INTO [dbo].[ServicePayments] (ServicePaymentId, ServiceOrderId, ServicePrice, PaymentDate, PaymentMethod)
VALUES 
('SP00000001', 'SR00000001', 350, '2024-03-24', 'MOMO'),
('SP00000002', 'SR00000002', 550, '2024-04-27', 'momo'),
('SP00000003', 'SR00000003', 1100, '2024-05-12', 'momo'),
('SP00000004', 'SR00000004', 70, '2024-05-15', 'momo'),
('SP00000005', 'SR00000005', 1000, '2024-05-26', 'momo');

GO

CREATE TABLE [ServiceOrderDetails] (
    [ServiceOrderId] char(10) NOT NULL,
    [ServiceId] int NOT NULL,
    CONSTRAINT [PK_ServiceOrderDetails] PRIMARY KEY ([ServiceOrderId], [ServiceId]),
    CONSTRAINT [FK_ServiceOrderDetails_ServiceOrders_ServiceOrderId] FOREIGN KEY ([ServiceOrderId]) REFERENCES [ServiceOrders] ([ServiceOrderId]) ON DELETE CASCADE,
    CONSTRAINT [FK_ServiceOrderDetails_Services_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [Services] ([ServiceId]) ON DELETE CASCADE
);
GO


INSERT INTO [dbo].[ServiceOrderDetails] (ServiceOrderId, ServiceId)
VALUES 
('SR00000001', 1),
('SR00000001', 2),
('SR00000002', 2),
('SR00000002', 3),
('SR00000003', 4),
('SR00000003', 5),
('SR00000004', 4),
('SR00000005', 5);

GO
CREATE INDEX [IX_Accounts_RoleId] ON [Accounts] ([RoleId]);
GO

CREATE INDEX [IX_AdmissionRecords_CageId] ON [AdmissionRecords] ([CageId]);
GO

CREATE INDEX [IX_AdmissionRecords_MedicalRecordId] ON [AdmissionRecords] ([MedicalRecordId]);
GO

CREATE INDEX [IX_AdmissionRecords_PetId] ON [AdmissionRecords] ([PetId]);
GO

CREATE INDEX [IX_AdmissionRecords_VeterinarianAccountId] ON [AdmissionRecords] ([VeterinarianAccountId]);
GO

CREATE INDEX [IX_Appointments_AccountId] ON [Appointments] ([AccountId]);
GO

CREATE INDEX [IX_Appointments_PetId] ON [Appointments] ([PetId]);
GO

CREATE INDEX [IX_Appointments_TimeSlotId] ON [Appointments] ([TimeSlotId]);
GO

CREATE INDEX [IX_Appointments_VeterinarianAccountId] ON [Appointments] ([VeterinarianAccountId]);
GO

CREATE INDEX [IX_BookingPayments_AppointmentId] ON [BookingPayments] ([AppointmentId]);
GO

CREATE INDEX [IX_Feedbacks_AccountId] ON [Feedbacks] ([AccountId]);
GO

CREATE INDEX [IX_MedicalRecords_AppointmentId] ON [MedicalRecords] ([AppointmentId]);
GO

CREATE INDEX [IX_MedicalRecords_PetId] ON [MedicalRecords] ([PetId]);
GO

CREATE INDEX [IX_PetHealthTracker_AdmissionRecordAdmissionId] ON [PetHealthTracker] ([AdmissionRecordAdmissionId]);
GO

CREATE INDEX [IX_PetHealthTracker_PetId] ON [PetHealthTracker] ([PetId]);
GO

CREATE INDEX [IX_Pets_AccountId] ON [Pets] ([AccountId]);
GO

CREATE INDEX [IX_ServiceOrders_MedicalRecordId] ON [ServiceOrders] ([MedicalRecordId]);
GO

CREATE UNIQUE INDEX [IX_ServicePayments_ServiceOrderId] ON [ServicePayments] ([ServiceOrderId]);
GO

CREATE INDEX [IX_ServiceOrderDetails_ServiceId] ON [ServiceOrderDetails] ([ServiceId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240521101714_FinalDb', N'8.0.5');
GO

COMMIT;
GO


