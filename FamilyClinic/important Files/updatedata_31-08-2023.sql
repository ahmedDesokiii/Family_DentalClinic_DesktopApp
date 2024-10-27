---------------------------- Patient_T----------------------------------------------------
-- Update Every nchar(n) data type to : nvarchar(n)
-- Add Col Name : patient_SessionCode : int
-- Add Col Name : patient_SessionNextDate : date
-- Add Col Name : patient_Follow : nvarchar(50)

-- Update in Data Base
Update Patient_T Set 
-- all Last Sessions Next Date Equal Setion Date for all patients.
patient_SessionNextDate = patient_SessionDate ,
-- all Last Sessions Code Equal 0
patient_SessionCode = 0 ,
-- all Last Patients Follow Equal Normal
patient_Follow = 'Normal'
---------------------------- Diagonsis_T---------------------------------------------------
-- Update Every nchar(n) data type to : nvarchar(n)
-- Add Col Name : dia_Code [add manualy From(0:15)]
---------------------------- Orthodontics_T----------------------------------------------
-- Update Every nchar(n) data type to : nvarchar(n)
-- Update Every datetime data type to : date
-- Add Col Name : orth_Follow : nvarchar(50)
-- Add Col Name : orth_RequiredAmount : float
-- Add Col Name : orth_RequiredAmount : int

Update Orthodontics_T Set  orth_Follow = 'Normal',
 orth_RequiredAmount = 0 ,
 orth_SessionCode = 0

  -- Remaining Equation : [orth_RemainingAmount] AS ([orth_RequiredAmount]-([orth_PaidAmount]+[orth_Discount])),