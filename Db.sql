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
    CONSTRAINT [PK_Cages] PRIMARY KEY ([CageId])
);
GO

CREATE TABLE [Roles] (
    [RoleId] int NOT NULL IDENTITY,
    [RoleName] nvarchar(10) NOT NULL,
    CONSTRAINT [PK_Roles] PRIMARY KEY ([RoleId])
);
GO

CREATE TABLE [Service] (
    [ServiceId] int NOT NULL IDENTITY,
    [ServicePrice] float NOT NULL,
    [ServiceName] nvarchar(50) NOT NULL,
    CONSTRAINT [PK_Service] PRIMARY KEY ([ServiceId])
);
GO

CREATE TABLE [TimeSlots] (
    [TimeSlotId] int NOT NULL IDENTITY,
    [StartTime] time NOT NULL,
    [EndTime] time NOT NULL,
    CONSTRAINT [PK_TimeSlots] PRIMARY KEY ([TimeSlotId])
);
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

CREATE TABLE [Feedbacks] (
    [FeedbackId] int NOT NULL IDENTITY,
    [Ratings] int NOT NULL,
    [FeedbackDetails] nvarchar(250) NULL,
    [AccountId] char(10) NULL,
    CONSTRAINT [PK_Feedbacks] PRIMARY KEY ([FeedbackId]),
    CONSTRAINT [FK_Feedbacks_Accounts_AccountId] FOREIGN KEY ([AccountId]) REFERENCES [Accounts] ([AccountId])
);
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

CREATE TABLE [Appointments] (
    [AppointmentId] char(10) NOT NULL,
    [AppointmentDate] date NOT NULL,
    [AppointmentType] nvarchar(50) NOT NULL,
    [AppointmentNotes] nvarchar(200) NOT NULL,
    [BookingPrice] float NOT NULL,
    [AppointmentStatus] nvarchar(15) NOT NULL,
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

CREATE TABLE [ServiceOrder] (
    [ServiceOrderId] char(10) NOT NULL,
    [Price] float NOT NULL,
    [OrderDate] date NOT NULL,
    [OrderStatus] nvarchar(50) NOT NULL,
    [MedicalRecordId] char(10) NOT NULL,
    CONSTRAINT [PK_ServiceOrder] PRIMARY KEY ([ServiceOrderId]),
    CONSTRAINT [FK_ServiceOrder_MedicalRecords_MedicalRecordId] FOREIGN KEY ([MedicalRecordId]) REFERENCES [MedicalRecords] ([MedicalRecordId]) ON DELETE CASCADE
);
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

CREATE TABLE [ServicePayment] (
    [ServicePaymentId] char(10) NOT NULL,
    [ServicePrice] float NOT NULL,
    [PaymentDate] date NOT NULL,
    [PaymentMethod] nvarchar(20) NOT NULL,
    [ServiceOrderId] char(10) NOT NULL,
    CONSTRAINT [PK_ServicePayment] PRIMARY KEY ([ServicePaymentId]),
    CONSTRAINT [FK_ServicePayment_ServiceOrder_ServiceOrderId] FOREIGN KEY ([ServiceOrderId]) REFERENCES [ServiceOrder] ([ServiceOrderId]) ON DELETE CASCADE
);
GO

CREATE TABLE [ServiceOrderDetails] (
    [ServiceOrderId] char(10) NOT NULL,
    [ServiceId] int NOT NULL,
    CONSTRAINT [PK_ServiceOrderDetails] PRIMARY KEY ([ServiceOrderId], [ServiceId]),
    CONSTRAINT [FK_ServiceOrderDetails_ServiceOrder_ServiceOrderId] FOREIGN KEY ([ServiceOrderId]) REFERENCES [ServiceOrder] ([ServiceOrderId]) ON DELETE CASCADE,
    CONSTRAINT [FK_ServiceOrderDetails_Service_ServiceId] FOREIGN KEY ([ServiceId]) REFERENCES [Service] ([ServiceId]) ON DELETE CASCADE
);
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

CREATE INDEX [IX_ServiceOrder_MedicalRecordId] ON [ServiceOrder] ([MedicalRecordId]);
GO

CREATE UNIQUE INDEX [IX_ServicePayment_ServiceOrderId] ON [ServicePayment] ([ServiceOrderId]);
GO

CREATE INDEX [IX_ServiceOrderDetails_ServiceId] ON [ServiceOrderDetails] ([ServiceId]);
GO

INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
VALUES (N'20240521093209_initial', N'8.0.5');
GO

COMMIT;
GO
