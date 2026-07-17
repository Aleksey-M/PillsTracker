namespace PillsTracker.Domain

open System

module CourseValidation =

    let validateDuration course =
        if course.PlannedDurationDays <= 0 then
            Error (InvalidDuration "Planned duration must be > 0")
        else
            Ok course

    let validateStartEnd course =
        match course.EndDate with
        | Some endDate when endDate < course.StartDate ->
            Error (InvalidDuration "End date cannot be earlier than start date")
        | _ -> Ok course



module DoseValidation =

    let validateNotTaken dose =
        match dose.ActualIntakeTime with
        | Some _ -> Error DoseAlreadyTaken
        | None -> Ok dose

    let validateMealRelation dose regimen lastMealTime =
        match regimen.MealRelation with
        | BeforeMeal ->
            if lastMealTime > dose.PlannedDate then
                Error (MealRelationViolation "Dose must be taken before meal")
            else Ok dose
        | WithMeal ->
            if abs ((dose.PlannedDate - lastMealTime).TotalMinutes) > 15.0 then
                Error (MealRelationViolation "Dose must be taken with meal")
            else Ok dose
        | AfterMeal ->
            if lastMealTime > dose.PlannedDate then
                Ok dose
            else Error (MealRelationViolation "Dose must be taken after meal")


module InteractionValidation =

    let validateGroups rule =
        if List.isEmpty rule.GroupA then
            Error EmptyGroupA
        elif List.isEmpty rule.GroupB then
            Error EmptyGroupB
        else
            Ok rule

    let validateNoOverlap rule =
        let overlap =
            rule.GroupA
            |> List.filter (fun a -> rule.GroupB |> List.contains a)

        match overlap with
        | [] -> Ok rule
        | xs -> Error (SamePreparationInBothGroups xs.Head)

    let validateDescription rule =
        if String.IsNullOrWhiteSpace(rule.Description) then
            Error (InvalidDescription "Description cannot be empty")
        else
            Ok rule

    let validateAll rule =
        rule
        |> validateGroups
        |> Result.bind validateNoOverlap
        |> Result.bind validateDescription
