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
INSERT INTO [dbo].[Roles] (RoleName)
VALUES ( N'Customer'),
       ( N'Admin'),
       ( N'Vet'),
       (N'Staff');

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

INSERT INTO [dbo].[Accounts] 
    (AccountId, Username, FullName, Password, IsMale, PhoneNumber, Email, DateOfBirth, JoinDate, IsDisabled, RoleId, Discriminator, ImgUrl, Experience, Description, Position, Department)
VALUES
('VE-00000000', 'thegoat3000', 'Tony Stark', '123456789', 1, '0937661777', 'tonystark@gmail.com', '1939-03-12', '2000-05-20', 0, 3, 'Veterinarian', 'https://sjkdlfj234.com/oij23lka/sdljf234jlk.html', 10, 'Handsome and straight', 'Senior', 'Surgery'),
('AC-00000001', 'user1', 'Gojo Satoru', '123456789', 1, '0937661771', 'user1@gmail.com', '1926-03-01', '2024-05-20', 0, 1, 'Account', null, null, null, null, null),
('AC-00000002', 'admin', 'John Wick', '123456789', 0, '0937661777', 'admin@gmail.com', '1929-03-01', '1960-05-20', 0, 2, 'Account', null, null, null, null, null),
('AC-00000003', 'user2', 'Nick Furry', '123456789', 1, '0937661777', 'user2@gmail.com', '1984-01-01', '2000-05-20', 0, 1, 'Account', null, null, null, null, null),
('VE-00000004', 'vet3', 'Peter Parker', '123456789', 0, '0937661777', 'vet3@gmail.com', '1984-01-01', '1999-05-20', 0, 3, 'Veterinarian', 'https://sjkdlfj234.com/oij23lka/sdljf234jlk.html', 10, 'Beautiful but gay', 'Junior', 'Surgery');
GO


INSERT INTO [dbo].[Feedbacks] 
    (AccountId, Ratings, [FeedbackDetails])
VALUES
('AC-00000001', 4, 'gud doctor'),
('AC-00000001', 2, 'gud doctor'),
('AC-00000001', 5, 'bad doctor'),
('AC-00000003', 1, 'very gud doctor'),
('AC-00000003', 2, 'very gud doctor');

GO

INSERT INTO [dbo].[Pets] (PetId, ImgUrl, PetName, PetBreed, PetAge, Description, IsMale, IsCat, VaccinationHistory, IsDisabled, AccountId)
VALUES
('PE-00000001', N'https://example.com/pet1.jpg', N'Buddy', N'Golden Retriever', 3, N'Friendly and active', 1, 0, N'Rabies, Distemper', 0, 'AC-00000003'),
('PE-00000002', N'https://example.com/pet2.jpg', N'Mittens', N'Siamese', 2, N'Quiet and affectionate', 0, 1, N'Rabies, Feline Leukemia', 0, 'AC-00000003'),
('PE-00000003', N'https://example.com/pet3.jpg', N'Rex', N'German Shepherd', 5, N'Loyal and protective', 1, 0, N'Rabies, Distemper, Parvovirus', 1, 'AC-00000003'),
('PE-00000004', N'https://example.com/pet4.jpg', N'Bella', N'Bulldog', 4, N'Gentle and calm', 0, 0, N'Rabies, Parvovirus', 0, 'AC-00000001'),
('PE-00000005', N'https://example.com/pet5.jpg', N'Simba', N'Maine Coon', 1, N'Playful and sociable', 1, 1, N'Rabies, Feline Leukemia, FIV', 0, 'AC-00000001');
GO

INSERT INTO [dbo].[Appointments] (AppointmentId, AccountId, AppointmentDate, PetId, VeterinarianAccountId, TimeSlotId, AppointmentType, AppointmentNotes, BookingPrice)
VALUES
('AP-00000001', 'AC-00000001', '2024-03-24', 'PE-00000001', 'VE-00000000', 1, 'Deposit', 'My pet is sneezing and watery eyes', 500000),
('AP-00000002', 'AC-00000001', '2024-04-27', 'PE-00000002', 'VE-00000004', 2, 'Deposit', 'My cat loss of appetite', 700000),
('AP-00000003', 'AC-00000001', '2024-05-12', 'PE-00000003', 'VE-00000000', 3, 'Deposit', 'I think my pet is stressed', 100000),
('AP-00000004', 'AC-00000003', '2024-05-15', 'PE-00000004', 'VE-00000004', 4, 'Deposit', 'Scratching, skin irritation', 1000000),
('AP-00000005', 'AC-00000003', '2024-05-26', 'PE-00000005', 'VE-00000000', 5, 'Deposit', 'Irregular bathroom habits, dehydration', 1000000);
GO

INSERT INTO [dbo].[BookingPayments] (PaymentId, Price, PaymentMethod, PaymentDate, AppointmentId)
VALUES
    ('BP-00000001', 500000, 'Momo', '2024-03-22', 'AP-00000001'),
	('BP-00000002', 1000000, 'BankTransfer', '2024-04-27', 'AP-00000002'),
	('BP-00000003', 500000, 'Cash', '2024-05-12', 'AP-00000001'),
	('BP-00000004', 1000000, 'Cash', '2024-05-15', 'AP-00000004'),
	('BP-00000005', 1000000, 'Cash', '2024-05-26', 'AP-00000005'),
	('BP-00000006', 1000000, 'Cash', '2024-05-26', 'AP-00000003');

GO
INSERT INTO [dbo].[MedicalRecords] 
    (MedicalRecordID, DateCreated, PetID, AppointmentID, PetWeight, Symptoms, Allergies, Diagnosis, AdditionalNotes, FollowUpAppointmentDate, FollowUpAppointmentNotes, DrugPrescriptions)
VALUES
('ME-00000001', '2024-03-24', 'PE-00000001', 'AP-00000001', 3, 'occasional sneezing, watery eyes', 'Not Known', 'mild allergic reaction to pollen', 'Suggested monitoring outdoor activity during high pollen count days.', '2024-03-27', 'Evaluate allergy response.', 'Antihistamines'),
('ME-00000002', '2024-04-27', 'PE-00000002', 'AP-00000002', 3.5, 'lethargy, loss of appetite', 'Not Known', 'gastritis', 'Keep on a bland diet, small servings until symptoms improve.', '2024-04-30', 'Check-up on recovery progress.', 'Probiotics, antacid medication.'),
('ME-00000003', '2024-05-12', 'PE-00000003', 'AP-00000003', 2.5, 'irregular grooming, stress behavior', 'Not Known', 'anxiety', 'Advised environmental changes and stress relief techniques.', '2024-05-15', 'Assess anxiety levels and effectiveness of interventions.', 'Mild sedative for high-stress situations.'),
('ME-00000004', '2024-05-15', 'PE-00000004', 'AP-00000004', 4, 'scratching, skin irritation', 'possible reaction to new dog food brand', 'dermatitis', 'Switched back to original dog food brand; use medicated shampoo for 2 weeks', '2024-05-18', 'Skin condition review.', 'Topical corticosteroid cream for affected areas.'),
('ME-00000005', '2024-05-26', 'PE-00000005', 'AP-00000005', 4, 'irregular bathroom habits, dehydration', 'Not Known', 'early-stage kidney disease', 'Hydration is critical; recommend wet food diet and regular water intake.', '2024-05-29', 'Review kidney function and adapt diet if needed.', 'Subcutaneous fluids as needed, renal support diet.')
GO
INSERT INTO [dbo].[AdmissionRecords] (AdmissionId, AdmissionDate, DischargeDate, IsDischarged, PetCurrentCondition, MedicalRecordId, VeterinarianAccountId, PetId, CageId)
VALUES
('AR-00000001', '2023-05-16', '2023-05-20', 1, N'Đã xuất viện', 'ME-00000001', 'VE-00000000', 'PE-00000001', 1),
('AR-00000002', '2024-05-13', NULL, 0, N'Đang ngủ', 'ME-00000002', 'VE-00000000', 'PE-00000002', 3),
('AR-00000003', '2024-05-18', '2024-05-20', 1, N'Đã xuất viện', 'ME-00000003', 'VE-00000000', 'PE-00000003', 4),
('AR-00000004', '2024-02-13', '2024-02-16', 1, N'Đã xuất viện', 'ME-00000004', 'VE-00000000', 'PE-00000004', 5),
('AR-00000005', '2024-05-20', NULL, 0, N'Đã ăn trưa', 'ME-00000005', 'VE-00000004', 'PE-00000005', 7);

GO
INSERT INTO [dbo].[ServiceOrders] (ServiceOrderId, Price, OrderDate, OrderStatus, MedicalRecordId)
VALUES 
('SR-00000001', 350, '2024-03-24', 'Paid', 'ME-00000001'),
('SR-00000002', 550, '2024-04-27', 'Paid', 'ME-00000002'),
('SR-00000003', 1100, '2024-05-12', 'Paid', 'ME-00000003'),
('SR-00000004', 70, '2024-05-15', 'Paid', 'ME-00000004'),
('SR-00000005', 1000, '2024-05-26', 'Paid', 'ME-00000005');

GO
INSERT INTO [dbo].[ServicePayments] (ServicePaymentId, ServiceOrderId, ServicePrice, PaymentDate, PaymentMethod)
VALUES 
('SP-00000001', 'SR00000001', 350, '2024-03-24', 'MOMO'),
('SP-00000002', 'SR00000002', 550, '2024-04-27', 'momo'),
('SP-00000003', 'SR00000003', 1100, '2024-05-12', 'momo'),
('SP-00000004', 'SR00000004', 70, '2024-05-15', 'momo'),
('SP-00000005', 'SR00000005', 1000, '2024-05-26', 'momo');

GO
INSERT INTO [dbo].[ServiceOrderDetails] (ServiceOrderId, ServiceId)
VALUES 
('SR-00000001', 1),
('SR-00000001', 2),
('SR-00000002', 2),
('SR-00000002', 3),
('SR-00000003', 4),
('SR-00000003', 5),
('SR-00000004', 4),
('SR-00000005', 5);
